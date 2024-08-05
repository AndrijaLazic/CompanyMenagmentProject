import { Routes } from '@angular/router';
import { LoginPageComponent } from './core/pages/login-page/login-page.component';
import { isAdminGuard } from './core/guards/is-admin.guard';
import { isWorkerGuard } from './core/guards/is-worker.guard';
import { UserProfilePageComponent } from './core/pages/user-profile-page/user-profile-page.component';
import { isLoggedInGuard } from './core/guards/is-logged-in.guard';
import { MessagingPageComponent } from './core/pages/messaging-page/messaging-page.component';

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
  {
    path: 'worker',
    canActivate: [isWorkerGuard],
    loadChildren: () =>
      import('./core/modules/worker-lazy-load/worker-lazy-load.module').then(
        (m) => m.WorkerLazyLoadModule,
      ),
  },
  {
    path: 'user',
    canActivate: [isLoggedInGuard],
    children: [
      {
        path: '',
        component: UserProfilePageComponent,
      },
      {
        path: 'messages',
        component: MessagingPageComponent,
      },
      {
        path: ':id',
        component: UserProfilePageComponent,
      },
    ],
  },
];
