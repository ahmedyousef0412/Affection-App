import { forgetPasswordRequest } from './../../../core/models/authentication/forgetPasswordRequest';
import { Component } from '@angular/core';
import { AuthService } from '../../../core/services/authService';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-forget-password',
  templateUrl: './forget-password.component.html',
  styleUrl: './forget-password.component.css'
})
export class ForgetPasswordComponent {


  credentials: forgetPasswordRequest = {
    email: ''
  };

  constructor(private authService: AuthService , private toastr:ToastrService ) { }
  
  reset(form: NgForm) {
    if (form.valid) {
      this.authService.sendResetPasswordCode(this.credentials).subscribe({
        next: () => {
          this.toastr.success('Password reset link sent! Check your email.');
          form.reset();
        },
        error: (err) => {
          this.toastr.error('Error sending password reset link. Please try again.' , err.message);
        }
      });
    }
  }
}
