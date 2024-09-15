import { TestBed } from '@angular/core/testing';
import { CanDeactivateFn } from '@angular/router';

import { prventUnsavedGuardGuard } from './prvent-unsaved-guard.guard';

describe('prventUnsavedGuardGuard', () => {
  const executeGuard: CanDeactivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => prventUnsavedGuardGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
