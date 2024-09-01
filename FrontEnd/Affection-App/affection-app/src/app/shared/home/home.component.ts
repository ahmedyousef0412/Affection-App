import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { authResponse } from '../../core/models/authentication/authResponse';
import { AuthService } from '../../core/services/authService';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit  {


  isLogged: boolean = false;
  currentUser$!: Observable<authResponse | null>;

  constructor(public authService: AuthService) {}

  ngOnInit(): void {
    this.currentUser$ = this.authService.currentUser$;
    this.currentUser$.subscribe(user => {
      this.isLogged = !!user;
    });
  }
}
