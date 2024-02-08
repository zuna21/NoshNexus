import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PdfService {
  download$ = new Subject<boolean>();
  constructor() { }

  onDownload() {
    this.download$.next(true);
  }
}
