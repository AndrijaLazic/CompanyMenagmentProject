import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from './core/pages/login-page/login-page.component';

export const routes: Routes = [
  {
    path: 'auth',
    children: [
      { path: 'login', component: LoginPageComponent },
      { path: '', redirectTo: '/auth/login', pathMatch: 'full' },
    ],
  },
];
