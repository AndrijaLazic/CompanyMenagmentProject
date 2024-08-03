import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  inject,
  Input,
  Output,
} from '@angular/core';
import { WorkResertvationDTO } from '../../../shared/models/DTO/WorkResertvationDTO';
import { TableModule } from 'primeng/table';
import { CalendarModule } from 'primeng/calendar';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
import { GlobalSettingsService } from '../../../shared/services/global-settings.service';
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
  @Input() workReservations: WorkResertvationDTO[] = [];
  @Input() editableTable: boolean = false;
  @Output() newDateEvent = new EventEmitter<Date>();
  globalSettingsSignal = inject(GlobalSettingsService).settings;

  selectedRows = [];
  shiftTypes: { label: string; value: number }[] = [];

  workerTypes: { label: string; value: number }[] = [];

  clonedReservations: { [s: string]: WorkResertvationDTO } = {};
  selectedDate: Date;
  constructor() {
    this.selectedDate = new Date();
    this.shiftTypes = [];
    this.workerTypes = [];

    this.globalSettingsSignal()?.shiftTypes.forEach((element) => {
      this.shiftTypes.push({
        label: element.startTime + ' - ' + element.endTime,
        value: element.shiftNumber,
      });
    });

    this.globalSettingsSignal()?.workerTypes.forEach((element) => {
      this.workerTypes.push({
        label: element.typeName,
        value: element.id,
      });
    });
  }

  onRowEditInit(product: WorkResertvationDTO) {
    this.clonedReservations[product.rowId] = { ...product };
  }

  onRowEditSave(product: WorkResertvationDTO) {
    delete this.clonedReservations[product.rowId.toString()];
  }

  onRowEditCancel(product: WorkResertvationDTO, index: number) {
    this.workReservations[index] =
      this.clonedReservations[product.rowId.toString()];
    delete this.clonedReservations[product.rowId.toString()];
  }

  ChangeDate(value: Date) {
    this.newDateEvent.emit(value);
  }

  removeRow() {
    this.selectedRows.forEach((element) => {
      const index = this.workReservations.indexOf(element, 0);
      if (index > -1) {
        this.workReservations.splice(index, 1);
      }
    });
    this.selectedRows = [];
  }

  formatShift(shiftId: number) {
    var res = this.globalSettingsSignal()?.shiftTypes.filter(
      (val) => val.shiftNumber == shiftId,
    );
    if (res == undefined || res[0] == undefined) return 'Error while formating';
    return res[0].startTime + ' - ' + res[0].endTime;
  }

  formatWorkerType(typeId: number) {
    var res = this.globalSettingsSignal()?.workerTypes.filter(
      (val) => val.id == typeId,
    );
    if (res == undefined || res[0] == undefined) return 'Error while formating';
    return res[0].typeName;
  }
}
