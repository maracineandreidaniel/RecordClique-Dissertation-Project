import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/services/authentication/authentication.service';
import { UserStoreService } from 'src/services/user-store/user-store.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public users: any = [];
  public fullName : string = "";
  public role! : string;
  public isAuthenticated! : boolean;
  constructor(private auth: AuthenticationService, private userStore: UserStoreService){}

  ngOnInit(): void {

    this.isAuthenticated = this.auth.isLoggedIn();

    this.userStore.getFullNameFromStore()
      .subscribe( val => {
      let fullNameFromToken = this.auth.getFullNameFromToken();
      this.fullName = val || fullNameFromToken;
    });

    this.userStore.getRoleFromStore()
      .subscribe( val => {
      let roleFromToken = this.auth.getRoleFromToken();
      this.role = val || roleFromToken;
      });

  }
}
