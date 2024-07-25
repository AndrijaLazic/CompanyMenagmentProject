import { Component } from '@angular/core';
import { NavBarComponent } from './core/components/nav-bar/nav-bar.component';
import { FootBarComponent } from './core/components/foot-bar/foot-bar.component';
import { RouterModule } from '@angular/router';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  standalone: true,
  imports: [RouterModule, NavBarComponent, FootBarComponent],
})
export class AppComponent {
  title = 'frontend-side-project';
}
