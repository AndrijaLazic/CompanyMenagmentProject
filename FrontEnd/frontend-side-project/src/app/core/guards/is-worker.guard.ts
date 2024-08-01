import { CanActivateFn } from '@angular/router';
import { GlobalUserStateService } from '../../shared/services/global-user-state.service';
import { inject } from '@angular/core';

export const isWorkerGuard: CanActivateFn = (route, state) => {
  const globalUserStateSerice = inject(GlobalUserStateService);
  if (globalUserStateSerice.currentUser() == null) return false;
  if (globalUserStateSerice.currentUser()?.workerType != 2) return false;
  return true;
};
