import { CanDeactivateFn } from '@angular/router';
import { CanComponentDeactivate } from '../models/canComponentDeactivate ';

export const prventUnsavedGuardGuard: CanDeactivateFn<CanComponentDeactivate> = (component, currentRoute, currentState, nextState) => {
  
  return component.canDeactivate ? component.canDeactivate() : true;
};
