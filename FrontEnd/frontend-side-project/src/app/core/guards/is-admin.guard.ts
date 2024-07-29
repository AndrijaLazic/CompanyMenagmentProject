import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { GlobalUserStateService } from '../../shared/services/global-user-state.service';

export const isAdminGuard: CanActivateFn = (route, state) => {
  const globalUserStateSerice = inject(GlobalUserStateService);
  if (globalUserStateSerice.currentUser() == null) return false;
  if (globalUserStateSerice.currentUser()?.workerType != 0) return false;
  return true;
};
