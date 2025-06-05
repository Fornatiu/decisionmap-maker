import { TestBed } from '@angular/core/testing';

import { QrMasterService } from './qr-master.service';

describe('QrMasterService', () => {
  let service: QrMasterService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(QrMasterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
