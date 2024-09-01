import { Component, OnInit } from '@angular/core';
import { resetPasswordRequest } from '../../../core/models/authentication/resestPasswordRequest';
import { ActivatedRoute, Router } from '@angular/router';

import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../../core/services/authService';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css'
})
export class ResetPasswordComponent implements OnInit {


  credential:resetPasswordRequest = {
    
    email: '',
    code: '',
    newPassword: ''
  }
  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private toastr: ToastrService,
    private router: Router
  ) {}


  ngOnInit(): void {
    // Extract the token and userId from the query parameters
    this.route.queryParams.subscribe(params => {
      this.credential.code = params['code'];
      this.credential.email = params['email'];
    });
  }

  reset(form: NgForm) {
    if (form.valid) {
      const request = {
        code: this.credential.code,
        email: this.credential.email,
        newPassword: this.credential.newPassword
      };

      this.authService.resetPassword(request).subscribe({
        next: () => {
          this.toastr.success('Password reset successfully!');
          this.router.navigate(['/login']);
        },
        error: (err) => {
          this.toastr.error('Error resetting password. Please try again.');
        }
      });
    }
  }
}
