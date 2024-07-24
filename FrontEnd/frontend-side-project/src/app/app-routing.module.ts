import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from './core/pages/login-page/login-page.component';

export const routes: Routes = [
  {
    path: 'auth',
    component: LoginPageComponent,
    // children: [
    //   { path: 'login', component: LoginPageComponent },
    //   { path: '', redirectTo: '/first-component', pathMatch: 'full' },
    // ],
  },
];
