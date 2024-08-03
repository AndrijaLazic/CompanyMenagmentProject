import { Component, inject } from '@angular/core';
import { NavBarComponent } from './core/components/nav-bar/nav-bar.component';
import { FootBarComponent } from './core/components/foot-bar/foot-bar.component';
import { RouterModule } from '@angular/router';
import { DatePipe } from '@angular/common';
import { GlobalSettingsService } from './shared/services/global-settings.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  standalone: true,
  imports: [RouterModule, NavBarComponent, FootBarComponent],
  providers: [DatePipe],
})
export class AppComponent {
  title = 'frontend-side-project';
  pom = inject(GlobalSettingsService);
}
