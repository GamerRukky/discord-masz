  import { Component, Input, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { LANGUAGES } from 'src/app/config/config';

@Component({
  selector: 'app-date-display',
  templateUrl: './date-display.component.html',
  styleUrls: ['./date-display.component.css']
})
export class DateDisplayComponent implements OnInit {

  @Input() prefixKey?: string = undefined;
  @Input() date?: Date;
  @Input() customFormat?: string = undefined;

  format: string = "d MMMM Y";

  constructor(private translator: TranslateService) { }

  ngOnInit(): void {
    console.log(this.prefixKey);
    
    if (this.customFormat !== undefined) {
      this.format = this.customFormat;
    } else {
      this.adjustFormat();
      this.translator.onLangChange.subscribe(() => {
        this.adjustFormat();
      });
    }
  }

  private adjustFormat() {
    let currentLang = this.translator.currentLang !== undefined ? this.translator.currentLang : this.translator.defaultLang;
    this.format = LANGUAGES.find(x => x.language === currentLang)?.dateFormat ?? "d MMMM Y";
  }
}