import { Directive, HostListener, Input } from '@angular/core';
import { NgControl } from '@angular/forms';

@Directive({
  selector: '[zipCode]',
  standalone: true
})
export class ZipCodeDirective {

  constructor(private ngControl: NgControl) { }

  @HostListener('input', ['$event'])
  onInput(event: Event) {
    const input = event.target as HTMLInputElement;
    const sanitizedValue = input.value.replace(/[^0-9$]/g, '');
    this.ngControl.control?.setValue(sanitizedValue);
  }

}
