import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { BehaviorSubject } from 'rxjs';
import { WorkResertvationDTR } from '../models/DTR/WorkResertvationDTR';

@Injectable({
  providedIn: 'root',
})
export class WorkCalendarService {
  private _httpClient = inject(HttpClient);

  GetMyWorkCalendar(dateString: string) {
    return this._httpClient.post(
      environment.BASE_URL + 'WorkCalendar/GetMyWorkCalendar',
      {
        dateString: dateString,
      },
    );
  }
}
