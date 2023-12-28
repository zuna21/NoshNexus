import {
  AfterViewInit,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnDestroy,
  Output,
  ViewChild,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import {
  fromEvent,
  filter,
  debounceTime,
  distinctUntilChanged,
  tap,
  Subscription,
} from 'rxjs';
import { SearchBarService } from './search-bar.service';

@Component({
  selector: 'app-search-bar',
  standalone: true,
  imports: [CommonModule, MatIconModule],
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css'],
})
export class SearchBarComponent implements AfterViewInit, OnDestroy {
  @ViewChild('input') input: ElementRef | undefined;
  @Input('dTime') dTime: number = 500;
  @Input('placeholder') placeholder: string = 'Search...';
  @Output('search') search = new EventEmitter<string>();

  isOnFocus: boolean = false;

  inputSub: Subscription | undefined;

  constructor(private searchBarService: SearchBarService) {}

  ngAfterViewInit() {
    this.onInput();
  }

  onInput() {
    if (!this.input) return;
    this.inputSub = fromEvent(this.input.nativeElement, 'keyup')
      .pipe(
        filter(Boolean),
        debounceTime(this.dTime),
        distinctUntilChanged(),
        tap((_) => {
          if (!this.input) return;
          const value = this.input.nativeElement.value;
          this.search.emit(value);
          this.searchBarService.searchQuery$.next(value);
        })
      )
      .subscribe();
  }

  ngOnDestroy(): void {
    this.inputSub?.unsubscribe();
  }
}
