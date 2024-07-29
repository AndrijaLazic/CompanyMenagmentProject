import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminLazyLoadRoutingModule } from './admin-lazy-load-routing.module';
import { WorkCalendarComponent } from '../../components/work-calendar/work-calendar.component';

@NgModule({
  declarations: [],
  imports: [CommonModule, AdminLazyLoadRoutingModule, WorkCalendarComponent],
})
export class AdminLazyLoadModule {}
