import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { authRequiredGuardGuard } from './auth-required-guard.guard';

describe('authRequiredGuardGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => authRequiredGuardGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
