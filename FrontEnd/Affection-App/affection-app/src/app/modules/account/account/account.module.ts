import { ShowPhotosComponent } from './../show-photos/show-photos.component';
import { NgModule } from '@angular/core';
import { EditProfileComponent } from '../edit-profile/edit-profile.component';
import { ProfileComponent } from '../profile/profile.component';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AccountRoutingModule } from './account-routing.module';
import { UploadPhotoComponent } from '../upload-photo/upload-photo.component';

@NgModule({
  declarations: [ProfileComponent, EditProfileComponent ,UploadPhotoComponent ,ShowPhotosComponent],
  imports: [
    CommonModule,
    RouterModule,
    AccountRoutingModule,
    ReactiveFormsModule,
    FormsModule
  ],
  
})
export class AccountModule {}
