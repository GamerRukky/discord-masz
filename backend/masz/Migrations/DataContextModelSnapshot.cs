﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using masz.data;

namespace masz.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("masz.Models.APIToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<byte[]>("TokenHash")
                        .HasColumnType("longblob");

                    b.Property<byte[]>("TokenSalt")
                        .HasColumnType("longblob");

                    b.Property<DateTime>("ValidUntil")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("APITokens");
                });

            modelBuilder.Entity("masz.Models.AutoModerationConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AutoModerationAction")
                        .HasColumnType("int");

                    b.Property<int>("AutoModerationType")
                        .HasColumnType("int");

                    b.Property<string>("CustomWordFilter")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("IgnoreChannels")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("IgnoreRoles")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("Limit")
                        .HasColumnType("int");

                    b.Property<int?>("PunishmentDurationMinutes")
                        .HasColumnType("int");

                    b.Property<int?>("PunishmentType")
                        .HasColumnType("int");

                    b.Property<bool>("SendDmNotification")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("SendPublicNotification")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("TimeLimitMinutes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AutoModerationConfigs");
                });

            modelBuilder.Entity("masz.Models.AutoModerationEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AssociatedCaseId")
                        .HasColumnType("int");

                    b.Property<int>("AutoModerationAction")
                        .HasColumnType("int");

                    b.Property<int>("AutoModerationType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Discriminator")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("MessageContent")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<ulong>("MessageId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Nickname")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Username")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("AutoModerationEvents");
                });

            modelBuilder.Entity("masz.Models.CaseTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CaseDescription")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CaseLabels")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("CasePunishedUntil")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CasePunishmentType")
                        .HasColumnType("int");

                    b.Property<string>("CaseTitle")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("CreatedForGuildId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("TemplateName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.Property<int>("ViewPermission")
                        .HasColumnType("int");

                    b.Property<bool>("announceDm")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("handlePunishment")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("sendPublicNotification")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("CaseTemplates");
                });

            modelBuilder.Entity("masz.Models.GuildConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AdminRoles")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("ExecuteWhoisOnJoin")
                        .HasColumnType("tinyint(1)");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("ModInternalNotificationWebhook")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("ModNotificationDM")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModPublicNotificationWebhook")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ModRoles")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("MutedRoles")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("PreferredLanguage")
                        .HasColumnType("int");

                    b.Property<bool>("PublishModeratorInfo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(true);

                    b.Property<bool>("StrictModPermissionCheck")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("GuildConfigs");
                });

            modelBuilder.Entity("masz.Models.GuildMotd", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Message")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("ShowMotd")
                        .HasColumnType("tinyint(1)");

                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.ToTable("GuildMotds");
                });

            modelBuilder.Entity("masz.Models.ModCase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("AllowComments")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(true);

                    b.Property<int>("CaseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreationType")
                        .HasColumnType("int");

                    b.Property<ulong>("DeletedByUserId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Discriminator")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Labels")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastEditedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("LastEditedByModId")
                        .HasColumnType("bigint unsigned");

                    b.Property<DateTime?>("LockedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("LockedByUserId")
                        .HasColumnType("bigint unsigned");

                    b.Property<DateTime?>("MarkedToDeleteAt")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("ModId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Nickname")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("OccuredAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Others")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("PunishedUntil")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("PunishmentActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("PunishmentType")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Username")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("Valid")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("ModCases");
                });

            modelBuilder.Entity("masz.Models.ModCaseComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Message")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("ModCaseId")
                        .HasColumnType("int");

                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.HasIndex("ModCaseId");

                    b.ToTable("ModCaseComments");
                });

            modelBuilder.Entity("masz.Models.UserInvite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("bigint unsigned");

                    b.Property<DateTime>("InviteCreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("InviteIssuerId")
                        .HasColumnType("bigint unsigned");

                    b.Property<DateTime>("JoinedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("JoinedUserId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("TargetChannelId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("UsedInvite")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("UserInvites");
                });

            modelBuilder.Entity("masz.Models.UserMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("CreatorUserId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Reason")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<ulong>("UserA")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("UserB")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.ToTable("UserMappings");
                });

            modelBuilder.Entity("masz.Models.UserNote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<ulong>("CreatorId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("bigint unsigned");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.ToTable("UserNotes");
                });

            modelBuilder.Entity("masz.Models.ModCaseComment", b =>
                {
                    b.HasOne("masz.Models.ModCase", "ModCase")
                        .WithMany("Comments")
                        .HasForeignKey("ModCaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
