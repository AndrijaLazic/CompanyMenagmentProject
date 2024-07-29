import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminPanelPageComponent } from '../../pages/admin-panel-page/admin-panel-page.component';

const routes: Routes = [
  {
    path: '',
    component: AdminPanelPageComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminLazyLoadRoutingModule {}
