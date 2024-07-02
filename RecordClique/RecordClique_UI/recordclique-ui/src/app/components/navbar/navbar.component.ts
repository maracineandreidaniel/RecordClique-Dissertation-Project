import { Component } from '@angular/core';
import { User } from 'src/app/models/user.model';
import { AuthenticationService } from 'src/services/authentication/authentication.service';
import { UserStoreService } from 'src/services/user-store/user-store.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent {
  public fullName: string = '';
  public role!: string;
  public isAuthenticated!: boolean;
  public todayDate!: Date;
  public user!: User;
  public initials!: string;

  constructor(
    private auth: AuthenticationService,
    private userStore: UserStoreService
  ) {}

  ngOnInit(): void {
    this.userStore.getFullNameFromStore().subscribe((val) => {
      let fullNameFromToken = this.auth.getFullNameFromToken();
      this.fullName = val || fullNameFromToken;
      this.isAuthenticated = this.fullName.length > 3;
    });

    this.userStore.getRoleFromStore().subscribe((val) => {
      let roleFromToken = this.auth.getRoleFromToken();
      this.role = val || roleFromToken;
    });

    if (this.isAuthenticated) {
      this.userStore.getUserDetails(this.fullName).subscribe((val) => {
        this.user = val || '';
      });

      this.userStore.getUserInitials(this.fullName).subscribe((val) => {
        this.initials = val || '';
      });
    }

    this.todayDate = new Date();
  }

  logOut() {
    this.auth.signOut();
  }
}
