import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ScrollService {
  scolledToBottom$ = new Subject<void>();

  constructor() { }
}
