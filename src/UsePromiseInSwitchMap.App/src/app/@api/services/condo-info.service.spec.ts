import { TestBed } from '@angular/core/testing';

import { CondoInfoService } from './condo-info.service';

describe('CondoInfoService', () => {
  let service: CondoInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CondoInfoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
