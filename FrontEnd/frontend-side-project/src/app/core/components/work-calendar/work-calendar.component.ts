import { Component, Input } from '@angular/core';
import { WorkResertvationDTR } from '../../../shared/models/DTR/WorkResertvationDTR';
import { TableModule } from 'primeng/table';
import { CalendarModule } from 'primeng/calendar';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-work-calendar',
  standalone: true,
  imports: [TableModule, CalendarModule, FormsModule],
  templateUrl: './work-calendar.component.html',
  styleUrl: './work-calendar.component.scss',
})
export class WorkCalendarComponent {
  @Input() workReservations: WorkResertvationDTR[] = [];
  @Input() editableTable: boolean = false;

  selectedDate: Date;
  constructor() {
    this.selectedDate = new Date();
  }
}
