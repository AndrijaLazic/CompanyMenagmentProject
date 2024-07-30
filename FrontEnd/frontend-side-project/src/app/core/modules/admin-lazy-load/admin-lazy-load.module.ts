import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminLazyLoadRoutingModule } from './admin-lazy-load-routing.module';
import { WorkCalendarComponent } from '../../components/work-calendar/work-calendar.component';
import { AdminPanelPageComponent } from '../../pages/admin-panel-page/admin-panel-page.component';
import { WorkCalendarService } from '../../../shared/services/work-calendar.service';

@NgModule({
  declarations: [AdminPanelPageComponent],
  imports: [CommonModule, AdminLazyLoadRoutingModule, WorkCalendarComponent],
})
export class AdminLazyLoadModule {}
