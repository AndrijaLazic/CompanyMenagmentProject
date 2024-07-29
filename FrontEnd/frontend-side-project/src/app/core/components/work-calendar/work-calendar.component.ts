import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-work-calendar',
  standalone: true,
  imports: [],
  templateUrl: './work-calendar.component.html',
  styleUrl: './work-calendar.component.scss',
})
export class WorkCalendarComponent {
  @Input() item = {
    name: 'Brzii',
  };
  promeni() {
    this.item.name = 'Andrija';
  }
}
