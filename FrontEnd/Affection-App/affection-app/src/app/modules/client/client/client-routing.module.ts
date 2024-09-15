import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MembersListComponent } from '../members-list/members-list.component';
import { MemberDetailsComponent } from '../member-details/member-details.component';
import { MemberPhotosComponent } from '../member-photos/member-photos.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'members', // Redirect to the members component by default
    pathMatch: 'full',
  },
  {
    path: 'members',
    component: MembersListComponent,
  },
  {
    path:'member/:id',
    component:MemberDetailsComponent
  },
  {
    path:'memberPhotos',
    component:MemberPhotosComponent
  },
  {
    path:'memberPhotos/:id',
    component:MemberPhotosComponent
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ClientRoutingModule {}
