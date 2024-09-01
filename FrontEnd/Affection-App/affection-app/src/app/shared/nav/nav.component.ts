import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../core/services/authService';
import { authResponse } from '../../core/models/authentication/authResponse';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent implements OnInit {
  isLogged: boolean = false;
  currentUser$!: Observable<authResponse | null>;

  constructor(public authService: AuthService) {}

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

