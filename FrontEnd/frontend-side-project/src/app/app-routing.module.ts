import { Routes } from '@angular/router';
import { LoginPageComponent } from './core/pages/login-page/login-page.component';
import { isAdminGuard } from './core/guards/is-admin.guard';

export const routes: Routes = [
  {
    path: 'auth',
    children: [
      { path: 'login', component: LoginPageComponent },
      { path: '', redirectTo: '/auth/login', pathMatch: 'full' },
    ],
  },
  {
    path: 'admin',
    canActivate: [isAdminGuard],
    loadChildren: () =>
      import('./core/modules/admin-lazy-load/admin-lazy-load.module').then(
        (m) => m.AdminLazyLoadModule,
      ),
  },
];
