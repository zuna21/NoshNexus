import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { TranslateService } from '@ngx-translate/core';
import { MatBottomSheetRef } from '@angular/material/bottom-sheet';

@Component({
  selector: 'app-language-bottom-sheet',
  standalone: true,
  imports: [CommonModule, MatIconModule, MatDividerModule],
  templateUrl: './language-bottom-sheet.component.html',
  styleUrls: ['./language-bottom-sheet.component.css'],
})
export class LanguageBottomSheetComponent {
  selectedLang: string | null = null;

  constructor(
    private translateService: TranslateService,
    private _bottomSheetRef: MatBottomSheetRef<LanguageBottomSheetComponent>
  ) {}

  ngOnInit(): void {
    this.setInitLanguage();
  }

  setInitLanguage() {
    this.selectedLang = localStorage.getItem('lang') ?? 'en';
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
    this._bottomSheetRef.dismiss();
  }
}
