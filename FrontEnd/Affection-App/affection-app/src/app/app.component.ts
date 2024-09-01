import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'affection-app';

  constructor(private router: Router) { }

  showNav(): boolean {
    const baseUrl = this.router.url.split('?')[0];
    // Hide the navigation on the login, register,  confirm email , forget password and reset password pages
    return !(baseUrl === '/login' ||
      baseUrl === '/register' ||
      baseUrl === '/auth/confirm-email' ||
      baseUrl === '/forgetPassword' ||
      baseUrl === '/auth/forget-password');
  }

}
