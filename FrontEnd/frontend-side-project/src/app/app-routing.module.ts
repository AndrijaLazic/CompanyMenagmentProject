import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from './core/pages/login-page/login-page.component';
import { AdminPanelPageComponent } from './core/pages/admin-panel-page/admin-panel-page.component';

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
    component: AdminPanelPageComponent,
  },
];
