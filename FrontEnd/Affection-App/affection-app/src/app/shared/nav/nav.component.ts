import { Component, inject, OnInit } from '@angular/core';

import { authResponse } from '../../core/models/authentication/authResponse';
import { Observable } from 'rxjs';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent implements OnInit {
  isLogged: boolean = false;
  currentUser$!: Observable<authResponse | null>;
  authService:AuthService = inject(AuthService);
  constructor() {}

  ngOnInit(): void {
    this.currentUser$ = this.authService.currentUser$;
    this.currentUser$.subscribe(user => {
      this.isLogged = !!user;
    });
  }

  logout(): void {
    this.authService.logout();
    this.isLogged = false;
  }
}

