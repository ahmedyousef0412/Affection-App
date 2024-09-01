import { Component, OnInit } from '@angular/core';
import { loginRequest } from '../../../core/models/authentication/loginRequest';
import { AuthService } from '../../../core/services/authService';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ErrorHandlingService } from '../../../core/services/error-handling.service';
import { Error } from '../../../core/models/result.model';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {


  credentials: loginRequest = {
    email: '',
    password: ''
  }
 

  constructor(private authService: AuthService,
     private router: Router,
      private toastr: ToastrService,
      private errorHandlingService: ErrorHandlingService) { }
  ngOnInit(): void {
    this.isLoggedIn();
  }


  login(form: NgForm): void {
    if (form.valid) {
      const request: loginRequest = {
        email: this.credentials.email,
        password: this.credentials.password,
      };
  
      this.authService.login(request).subscribe({
        next: (res) => {
          // this.toastr.success('Login Successfully');
          this.router.navigate(['/home']);
        },
        error: (err: HttpErrorResponse) => {
          // Handle the error here
          const errorMessage = err.statusText;
          this.toastr.error(errorMessage.toString());
        },
      });
    }
  }


  


  

  






  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }
}
