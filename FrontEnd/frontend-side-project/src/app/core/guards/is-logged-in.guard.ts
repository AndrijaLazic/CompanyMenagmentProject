import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { GlobalUserStateService } from '../../shared/services/global-user-state.service';

export const isLoggedInGuard: CanActivateFn = (route, state) => {
  const globalUserStateSerice = inject(GlobalUserStateService);
  if (globalUserStateSerice.currentUser() == null) return false;
  return true;
};
