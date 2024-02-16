import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-language-btn',
  standalone: true,
  imports: [CommonModule, MatIconModule, MatButtonModule, MatMenuModule],
  templateUrl: './language-btn.component.html',
  styleUrls: ['./language-btn.component.css'],
})
export class LanguageBtnComponent implements OnInit {
  selectedLang: string | null = null;

  constructor(private translateService: TranslateService) {}

  ngOnInit(): void {
    this.setInitLanguage();
  }

  setInitLanguage() {
    this.selectedLang = localStorage.getItem('lang');
    if (
      this.selectedLang &&
      this.translateService.getLangs().some((x) => x === this.selectedLang)
    ) {
      this.translateService.use(this.selectedLang);
    }
  }

  onSelectLang(lang: string) {
    this.selectedLang = lang;
    if (!this.translateService.getLangs().some((x) => x === this.selectedLang))
      return;
    this.translateService.use(this.selectedLang);
    localStorage.setItem('lang', this.selectedLang);
  }
}
