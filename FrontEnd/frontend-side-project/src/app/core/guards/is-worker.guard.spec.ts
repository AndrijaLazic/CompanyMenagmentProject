import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { isWorkerGuard } from './is-worker.guard';

describe('isWorkerGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) =>
    TestBed.runInInjectionContext(() => isWorkerGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
