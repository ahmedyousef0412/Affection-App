import { Component, inject, OnInit } from '@angular/core';
import { UserProfile } from './../../../core/models/account/userProfile';
import { AccountService } from '../../../core/services/account.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {


  profile: UserProfile = {} as UserProfile;
  accountService:AccountService = inject(AccountService);

  constructor() { }


  ngOnInit() {
    this.userProfile();
  }


  userProfile() {
    this.accountService.userProfile().subscribe((res) => {
      this.profile = res;
      console.log(this.profile);

    });
  }
}
