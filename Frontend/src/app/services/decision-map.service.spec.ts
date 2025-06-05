import { TestBed } from '@angular/core/testing';

import { DecisionMapService } from './decision-map.service';

describe('DecisionMapService', () => {
  let service: DecisionMapService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DecisionMapService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
