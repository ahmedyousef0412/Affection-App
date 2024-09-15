import { Component, inject, OnInit } from '@angular/core';
import { resetPasswordRequest } from '../../../core/models/authentication/resestPasswordRequest';
import { ActivatedRoute, Router } from '@angular/router';

import { ToastrService } from 'ngx-toastr';

import { NgForm } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css'
})
export class ResetPasswordComponent implements OnInit {


  authService:AuthService = inject(AuthService);
  credential:resetPasswordRequest = {
    
    email: '',
    code: '',
    newPassword: ''
  }
  constructor(
    private route: ActivatedRoute,
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
          this.router.navigate(['auth/login']);
        },
        error: (err) => {
          this.toastr.error('Error resetting password. Please try again.');
        }
      });
    }
  }
}
