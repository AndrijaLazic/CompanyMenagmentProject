import { Component, inject } from '@angular/core';
import { GlobalUserStateService } from '../../../shared/services/global-user-state.service';
import { ButtonModule } from 'primeng/button';
import { ActivatedRoute } from '@angular/router';
import { User } from '../../../shared/models/User';
@Component({
  selector: 'app-user-profile-page',
  standalone: true,
  imports: [ButtonModule],
  templateUrl: './user-profile-page.component.html',
  styleUrl: './user-profile-page.component.scss',
})
export class UserProfilePageComponent {
  globalStateService = inject(GlobalUserStateService);
  private activatedRoute = inject(ActivatedRoute);
  userId: string | null = null;
  user: User | null = null;

  ngOnInit(): void {
    this.userId = this.activatedRoute.snapshot.paramMap.get('id');
    console.log(this.userId);
    if (this.userId) {
      this.user = {
        name: 'Korisnik1',
        lastname: 'Korisnik1',
        email: 'Korisnik1@gmail.com',
        id: Number.parseInt(this.userId),
        phoneNumber: '1231231',
        jwt: '',
        workerType: 1,
      };
      return;
    }
    this.user = this.globalStateService.currentUser();
  }
}
