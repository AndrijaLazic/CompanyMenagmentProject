import { Component } from '@angular/core';
import { WorkCalendarComponent } from '../../components/work-calendar/work-calendar.component';

@Component({
  selector: 'app-admin-panel-page',
  standalone: true,
  imports: [WorkCalendarComponent],
  templateUrl: './admin-panel-page.component.html',
  styleUrl: './admin-panel-page.component.scss',
})
export class AdminPanelPageComponent {
  item = {
    name: 'Brzii',
  };
}
