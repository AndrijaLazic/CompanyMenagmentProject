import { Component, Input } from '@angular/core';
import { WorkResertvationDTR } from '../../../shared/models/DTR/WorkResertvationDTR';
import { TableModule } from 'primeng/table';
import { CalendarModule } from 'primeng/calendar';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
@Component({
  selector: 'app-work-calendar',
  standalone: true,
  imports: [
    TableModule,
    CalendarModule,
    FormsModule,
    CommonModule,
    DropdownModule,
    ButtonModule,
  ],
  templateUrl: './work-calendar.component.html',
  styleUrl: './work-calendar.component.scss',
})
export class WorkCalendarComponent {
  @Input() workReservations: WorkResertvationDTR[] = [];
  @Input() editableTable: boolean = false;

  shiftTypes = [
    { label: '06-14', value: 1 },
    { label: '14-20', value: 2 },
  ];

  workerTypes = [
    { label: 'Admin', value: 0 },
    { label: 'Menager', value: 1 },
    { label: 'Worker', value: 2 },
  ];

  clonedReservations: { [s: string]: WorkResertvationDTR } = {};
  selectedDate: Date;
  constructor() {
    this.selectedDate = new Date();
  }

  onRowEditInit(product: WorkResertvationDTR) {
    this.clonedReservations[product.rowId] = { ...product };
  }

  onRowEditSave(product: WorkResertvationDTR) {
    delete this.clonedReservations[product.rowId.toString()];
  }

  onRowEditCancel(product: WorkResertvationDTR, index: number) {
    this.workReservations[index] =
      this.clonedReservations[product.rowId.toString()];
    delete this.clonedReservations[product.rowId.toString()];
  }
}
