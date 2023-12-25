import { TestBed } from '@angular/core/testing';

import { SharedCardsService } from './shared-cards.service';

describe('SharedCardsService', () => {
  let service: SharedCardsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SharedCardsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
