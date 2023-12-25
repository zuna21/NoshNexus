import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SharedCardsComponent } from './shared-cards.component';

describe('SharedCardsComponent', () => {
  let component: SharedCardsComponent;
  let fixture: ComponentFixture<SharedCardsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SharedCardsComponent]
    });
    fixture = TestBed.createComponent(SharedCardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
