import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'app-foot-bar',
  standalone: true,
  imports: [],
  templateUrl: './foot-bar.component.html',
  styleUrl: './foot-bar.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class FootBarComponent {}
