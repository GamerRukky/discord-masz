using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using masz.Enums;
using masz.Exceptions;
using masz.Models;
using masz.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace masz.Controllers
{
    [ApiController]
    [Route("api/v1/guilds/{guildId}/dashboard")]
    [Authorize]
    public class GuildDashbordController : SimpleController
    {
        private readonly ILogger<GuildDashbordController> _logger;
        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public GuildDashbordController(ILogger<GuildDashbordController> logger, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;
        }

        [HttpGet("chart")]
        public async Task<IActionResult> GetModCaseGrid([FromRoute] ulong guildId, [FromQuery] long? since = null)
        {
            await RequirePermission(guildId, DiscordPermission.Moderator);
            Identity identity = await GetIdentity();

            DateTime sinceTime = DateTime.UtcNow.AddYears(-1);
            if (since != null) {
                sinceTime = epoch.AddSeconds(since.Value);
            }

            ModCaseRepository modCaseRepo = ModCaseRepository.CreateDefault(_serviceProvider, identity);
            AutoModerationEventRepository automodRepo = AutoModerationEventRepository.CreateDefault(_serviceProvider);
            return Ok( new {
                modCases = await modCaseRepo.GetCounts(guildId, sinceTime),
                punishments = await modCaseRepo.GetPunishmentCounts(guildId, sinceTime),
                autoModerations = await automodRepo.GetCounts(guildId, sinceTime)
            });
        }

        [HttpGet("automodchart")]
        public async Task<IActionResult> GetAutomodSplitChart([FromRoute] ulong guildId, [FromQuery] long? since = null)
        {
            await RequirePermission(guildId, DiscordPermission.Moderator);
            Identity identity = await GetIdentity();

            DateTime sinceTime = DateTime.UtcNow.AddYears(-1);
            if (since != null) {
                sinceTime = epoch.AddSeconds(since.Value);
            }

            AutoModerationEventRepository automodRepo = AutoModerationEventRepository.CreateDefault(_serviceProvider);

            return Ok(await automodRepo.GetCountsByType(guildId, sinceTime));
        }

        [HttpGet("stats")]
        public async Task<IActionResult> Stats([FromRoute] ulong guildId)
        {
            await RequirePermission(guildId, DiscordPermission.Moderator);
            Identity identity = await GetIdentity();

            ModCaseRepository modCaseRepo = ModCaseRepository.CreateDefault(_serviceProvider, identity);
            int modCases = await modCaseRepo.CountAllCasesForGuild(guildId);
            int activePunishments = await modCaseRepo.CountAllPunishmentsForGuild(guildId);
            int activeBans = await modCaseRepo.CountAllActiveBansForGuild(guildId);
            int activeMutes = await modCaseRepo.CountAllActiveMutesForGuild(guildId);
            int autoModerations = await AutoModerationEventRepository.CreateDefault(_serviceProvider).CountEventsByGuild(guildId);
            int trackedInvites = await InviteRepository.CreateDefault(_serviceProvider).CountInvitesForGuild(guildId);
            int userMappings = await UserMapRepository.CreateDefault(_serviceProvider, identity).CountAllUserMapsByGuild(guildId);
            int userNotes = await UserNoteRepository.CreateDefault(_serviceProvider, identity).CountUserNotesForGuild(guildId);
            int comments = await ModCaseCommentRepository.CreateDefault(_serviceProvider, identity).CountCommentsByGuild(guildId);

            return Ok(new {
                caseCount = modCases,
                activeCount = activePunishments,
                activeBanCount = activeBans,
                activeMuteCount = activeMutes,
                moderationCount = autoModerations,
                trackedInvites = trackedInvites,
                userMappings = userMappings,
                userNotes = userNotes,
                comments = comments
            });
        }

        [HttpGet("latestcomments")]
        public async Task<IActionResult> LatestComments([FromRoute] ulong guildId)
        {
            await RequirePermission(guildId, DiscordPermission.Moderator);
            Identity identity = await GetIdentity();

            List<CommentExpandedView> view = new List<CommentExpandedView>();
            foreach (ModCaseComment comment in await ModCaseCommentRepository.CreateDefault(_serviceProvider, identity).GetLastCommentsByGuild(guildId)) {
                view.Add(new CommentExpandedView(comment, await _discordAPI.FetchUserInfo(comment.UserId, CacheBehavior.OnlyCache)));
            }

            return Ok(view);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromRoute] ulong guildId, [FromQuery] string search)
        {
            await RequirePermission(guildId, DiscordPermission.Moderator);
            Identity identity = await GetIdentity();

            if (String.IsNullOrWhiteSpace(search)) {
                return Ok(new List<string>());
            }

            List<QuickSearchEntry> entries = new List<QuickSearchEntry>();

            foreach (ModCase item in await ModCaseRepository.CreateDefault(_serviceProvider, identity).SearchCases(guildId, search))
            {
                entries.Add(new QuickSearchEntry<CaseView> {
                    Entry = new CaseView(item),
                    CreatedAt = item.CreatedAt,
                    QuickSearchEntryType = QuickSearchEntryType.ModCase
                });
            }

            foreach (AutoModerationEvent item in await AutoModerationEventRepository.CreateDefault(_serviceProvider).SearchInGuild(guildId, search))
            {
                entries.Add(new QuickSearchEntry<AutoModerationEventView> {
                    Entry = new AutoModerationEventView(item),
                    CreatedAt = item.CreatedAt,
                    QuickSearchEntryType = QuickSearchEntryType.AutoModeration
                });
            }


            UserNoteExpandedView userNote = null;
            try
            {
                ulong userId = ulong.Parse(search);
                UserNote note = await UserNoteRepository.CreateDefault(_serviceProvider, identity).GetUserNote(guildId, userId);
                userNote = new UserNoteExpandedView(
                    note,
                    await _discordAPI.FetchUserInfo(note.UserId, CacheBehavior.OnlyCache),
                    await _discordAPI.FetchUserInfo(note.CreatorId, CacheBehavior.OnlyCache)
                );
            } catch (ResourceNotFoundException) { }

            List<UserMappingExpandedView> userMappingViews = new List<UserMappingExpandedView>();
            try
            {
                ulong userId = ulong.Parse(search);
                List<UserMapping> userMappings = await UserMapRepository.CreateDefault(_serviceProvider, identity).GetUserMapsByGuildAndUser(guildId, userId);
                foreach (UserMapping userMapping in userMappings)
            {
                userMappingViews.Add(new UserMappingExpandedView(
                    userMapping,
                    await _discordAPI.FetchUserInfo(userMapping.UserA, CacheBehavior.OnlyCache),
                    await _discordAPI.FetchUserInfo(userMapping.UserB, CacheBehavior.OnlyCache),
                    await _discordAPI.FetchUserInfo(userMapping.CreatorUserId, CacheBehavior.OnlyCache)
                ));
            }
            } catch (ResourceNotFoundException) { }

            return Ok(new {
                searchEntries = entries.OrderByDescending(x => x.CreatedAt).ToList(),
                userNoteView = userNote,
                userMappingViews = userMappingViews
            });
        }
    }
}