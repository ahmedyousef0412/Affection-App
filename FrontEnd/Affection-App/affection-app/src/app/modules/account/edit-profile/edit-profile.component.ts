import { Component, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../../../core/services/account.service';
import { UserProfile } from '../../../core/models/account/userProfile';
import { NgForm } from '@angular/forms';
import { ChangePasswordRequest } from '../../../core/models/account/changePassword';
import { CanComponentDeactivate } from '../../../shared/models/canComponentDeactivate ';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css',
})
export class EditProfileComponent implements OnInit , CanComponentDeactivate {

  user: UserProfile = {} as UserProfile;
  @ViewChild('editForm') editForm!: NgForm;


  //Show dialog when user edit any value and leave the page before save changes.
  // @HostListener('window:beforeunload', ['$event']) beforeLoad($event: any) {
  //   if (this.editForm.dirty) {
  //     $event.returnValue = true;
  //   }
  // }

  showPasswordPanel: boolean = false;


  userPassword: ChangePasswordRequest = {
    currentPassword: '',
    newPassword: ''
  }

 
  togglePasswordChange(): void {
    this.showPasswordPanel = !this.showPasswordPanel;
  }


  errorMessage: string = ''; // Property to hold error message
  constructor(
    private accountService: AccountService,
    private toastr: ToastrService
  ) { }



 
  ngOnInit(): void {
    this.loadUserProfile();
  }

  loadUserProfile() {
    this.accountService.userProfile().subscribe((res) => {
      this.user = res;
    });
  }
  editProfile() {
    this.accountService.updateProfile(this.user).subscribe(() => {

      this.toastr.success(" Profile Updated Successfully...")
    });
  }

  changePassword() {
    if (this.userPassword.currentPassword === this.userPassword.newPassword) {
      this.errorMessage = 'New password cannot be the same as the current password.';
      return;
    }
  
    this.errorMessage = ''; // Clear any previous error message
  
    this.accountService.changePassword(this.userPassword).subscribe(
      () => {
        this.toastr.success('Password changed successfully');
        this.resetPasswordForm(); 
        this.togglePasswordChange(); // Close the password change panel
      },
      (error) => {
        this.errorMessage = 'An error occurred while changing the password.';
        console.error(error);
      }
    );
  }
  
  

  canDeactivate(): Observable<boolean> | Promise<boolean> | boolean {
    if (this.editForm.dirty) {
      return confirm('You have unsaved changes. Do you really want to leave?');
    }
    return true;
  }


  resetPasswordForm() {
    this.userPassword.currentPassword = '';
    this.userPassword.newPassword = '';

  }
  

}
