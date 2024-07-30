import { Component, inject } from '@angular/core';
import { WorkCalendarComponent } from '../../components/work-calendar/work-calendar.component';
import { WorkResertvationDTR } from '../../../shared/models/DTR/WorkResertvationDTR';
import { WorkCalendarService } from '../../../shared/services/work-calendar.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-admin-panel-page',
  templateUrl: './admin-panel-page.component.html',
  styleUrl: './admin-panel-page.component.scss',
})
export class AdminPanelPageComponent {
  private workCalendarService = inject(WorkCalendarService);

  currentDateString: string = '';

  myWorkCalendar: WorkResertvationDTR[] = [];
  constructor() {
    const now = new Date();
    const datePipe = inject(DatePipe);
    let dateString = datePipe.transform(now, 'yyyy-MM-dd');
    if (dateString) {
      this.currentDateString = dateString;
    }

    this.workCalendarService.GetMyWorkCalendar('2024-01-06').subscribe({
      next: (response: any) => {
        this.myWorkCalendar = response.data;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
