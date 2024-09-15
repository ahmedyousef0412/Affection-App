
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Result } from '../../../core/models/result.model';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrl: './confirm-email.component.css'
})
export class ConfirmEmailComponent implements OnInit {



  message: string = '';
  authService:AuthService = inject(AuthService);
  
  constructor(private route: ActivatedRoute,  private router: Router) { }


  ngOnInit(): void {
    // Get query parameters
    this.route.queryParams.subscribe(params => {
      const userId = params['userId'];
      const code = params['code'];

      if (userId && code) {
        const request = {
          userId: userId,
          code: code
        };

        this.authService.confirmEmail(request).subscribe({
          next: (res: Result) => {
            this.message = 'Email confirmed successfully! Click here to ';
            // Optionally redirect to login or another page
            // this.router.navigate(['/login']);
          },
          error: (err) => {
            this.message = `Email confirmation failed: ${err.error.message || 'Unknown error'}`;
          }
        });
      } else {
        this.message = 'Invalid confirmation link.';
      }
    });
  }
}


