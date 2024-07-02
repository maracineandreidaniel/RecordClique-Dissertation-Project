import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { UserStoreService } from 'src/services/user-store/user-store.service';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css']
})
export class MyProfileComponent {
  user!: User;

  constructor(
    private route: ActivatedRoute,
    private userStore: UserStoreService
    ){}

  ngOnInit (){
    var username = this.route.snapshot.paramMap.get('username') || '';
    this.userStore.getUserDetails(username)
    .subscribe({
        next: (res) => {
             this.user = res;
        },
        error: (err) => {
            console.log(err);
        }
    });
  }

}
