import { ShowPhotosComponent } from './../show-photos/show-photos.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileComponent } from '../profile/profile.component';
import { EditProfileComponent } from '../edit-profile/edit-profile.component';
import { UploadPhotoComponent } from '../upload-photo/upload-photo.component';
import { prventUnsavedGuardGuard } from '../../../shared/guards/prvent-unsaved-guard.guard';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'profile', // Redirect to the profile component by default
    pathMatch: 'full'
  },
  {
    path: 'profile',
    component: ProfileComponent,
  },
  {
    path: 'editprofile',
    component: EditProfileComponent,
    canDeactivate:[prventUnsavedGuardGuard]
  },
  {
    path: 'uploadPhoto',
    component: UploadPhotoComponent,
    canDeactivate:[prventUnsavedGuardGuard]
  },
  {
    path: 'showPhotos',
    component: ShowPhotosComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AccountRoutingModule { }
