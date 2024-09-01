import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../core/services/authService';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrl: './members.component.css'
})
export class MembersComponent implements OnInit {

  memberMessage: any = '';
  constructor(private authService: AuthService) {
    this.getMemberMessage();

    
  }
  ngOnInit(): void {
    this.getMemberMessage();
  }



  getMemberMessage() {
    this.authService.getMembers().subscribe(
      data => {
        // Assign the response data to memberMessage
        this.memberMessage = data;
      },
      error => {
        // Handle errors if needed
        console.error('Error fetching members:', error);
      }
    );
  }
}
