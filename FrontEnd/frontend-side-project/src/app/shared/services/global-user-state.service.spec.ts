import { TestBed } from '@angular/core/testing';

import { GlobalUserStateService } from './global-user-state.service';

describe('GlobalUserStateService', () => {
  let service: GlobalUserStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GlobalUserStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
