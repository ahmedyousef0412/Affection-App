import { Component, inject } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../../core/services/auth.service';
import { CanComponentDeactivate } from '../../../shared/models/canComponentDeactivate ';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent  implements CanComponentDeactivate {


  authService:AuthService = inject(AuthService);
  registerData = {
    email: '',
    password: '',
    confirmPassword: '',
    gender: '',
    dateOfBirth: '',
    knowAs: '',
    userName: '',
    country: '',
    city: ''
  };
  
 constructor( private router:Router ,private toastr: ToastrService ){}

 startDate = new Date(1996, 1, 1);

 isLoading = false;

onSubmit(form: NgForm) {
  if (form.valid) {
    this.isLoading = true;
    this.authService.register(form.value).subscribe({
      next: (res) => {
        this.router.navigateByUrl('auth/login');
        form.reset();
        this.toastr.success("Register Successfully. Please check your email to confirm email.");
        this.isLoading = false;
      },
      error: (err) => {
        this.isLoading = false;
        let errorMessage = 'An error occurred. Please try again.';
        if (err.error && err.error.message) {
          errorMessage = err.error.message;
        } else if (err.error && Array.isArray(err.error)) {
          errorMessage = err.error.join(' ');
        } else if (err.message) {
          errorMessage = err.message;
        }
        this.toastr.error(errorMessage, 'Registration Error');
      }
    });
  }
}


  passwordsMatch(password: string, confirmPassword: string): boolean {
    return password === confirmPassword;
  }

  futureDateError = false;

  validateDateOfBirth() {
    const today = new Date();
    const selectedDate = new Date(this.registerData.dateOfBirth);

    if (selectedDate > today) {
      this.futureDateError = true;
      
    } else {
      this.futureDateError = false;
      
    }
  }


  canDeactivate(): Observable<boolean> | Promise<boolean> | boolean {
    if (this.isFormDirty()) {
      return confirm('You have unsaved changes. Do you really want to leave?');
    }
    return true;
  }

  private isFormDirty(): boolean {
    return !!Object.values(this.registerData).find(field => field); // Returns true if any field has a value
  }
}
