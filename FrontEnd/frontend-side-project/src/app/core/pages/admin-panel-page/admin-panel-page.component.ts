import { Component, inject } from '@angular/core';
import { WorkResertvationDTO } from '../../../shared/models/DTO/WorkResertvationDTO';
import { WorkCalendarService } from '../../../shared/services/work-calendar.service';
import { DatePipe } from '@angular/common';
import { GlobalSettingsService } from '../../../shared/services/global-settings.service';

@Component({
  selector: 'app-admin-panel-page',
  templateUrl: './admin-panel-page.component.html',
  styleUrl: './admin-panel-page.component.scss',
})
export class AdminPanelPageComponent {
  private workCalendarService = inject(WorkCalendarService);
  private datePipe = inject(DatePipe);
  currentDateString: string = '';

  workCalendar: WorkResertvationDTO[] = [];
  constructor() {
    const now = new Date();
    let dateString = this.datePipe.transform(now, 'MM-dd-yyyy');
    if (dateString) {
      this.currentDateString = dateString;
    }
    this.getWorkForDate('07-31-2024');
  }

  newDateEvent(event: Date) {
    let dateString = this.datePipe.transform(event, 'MM-dd-yyyy');
    this.getWorkForDate(dateString!);
  }

  private getWorkForDate(dateString: string) {
    this.workCalendarService
      .GetWorkCalendarForAllUsers(dateString, 0, 10)
      .subscribe({
        next: (response: any) => {
          this.workCalendar = response.data;
        },
        error: (error) => {
          console.log(error);
        },
      });
  }
}
