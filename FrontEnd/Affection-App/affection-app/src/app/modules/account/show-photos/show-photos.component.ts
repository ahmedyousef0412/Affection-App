import { UserProfile } from './../../../core/models/account/userProfile';
import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../../../core/services/account.service';
import { PhotoResponse } from '../../../core/models/account/photoResponse';
import { ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../../shared/confirm-dialog/confirm-dialog.component';
import { AuthService } from '../../../core/services/auth.service';
import { take } from 'rxjs';
import { authResponse } from '../../../core/models/authentication/authResponse';


@Component({
  selector: 'app-show-photos',
  templateUrl: './show-photos.component.html',
  styleUrl: './show-photos.component.css'
})
export class ShowPhotosComponent implements OnInit {
  
  photos: PhotoResponse[] = [];
  accountService:AccountService = inject(AccountService);
  authService:AuthService = inject(AuthService);
  userProfile:UserProfile = {} as UserProfile;
  user:authResponse;

  constructor(private dialog: MatDialog,private toastr:ToastrService){

    this.authService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.loadPhotos();

  }


  loadPhotos() {
    this.accountService.getPhotos().subscribe((res) =>{
      this.photos = res
    })
  }

  setPhotoMain(photoId: number){
    this.accountService.setPhotoMain(photoId).subscribe(()=>{
      const mainPhotoUrl = this.photos.find(p => p.id == photoId).url;

      this.user.photoUrl  = mainPhotoUrl;
     
      this.authService.setCurrentUser(this.user);
      this.userProfile.mainPhotoUrl = mainPhotoUrl;
      this.loadPhotos();
      
    })
  }
  deletePhoto(photoId: number) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: { message: 'Are you sure you want to delete this photo?' },
      panelClass: 'custom-dialog-container' 
    });
    
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.accountService.deletePhoto(photoId).subscribe(() => {
          this.photos = this.photos.filter(p => p.id !== photoId);
          this.toastr.success("Photo deleted successfully")
        });
      }
    });
  }
}
 

