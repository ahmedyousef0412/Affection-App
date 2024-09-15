import { MembersListComponent } from './../members-list/members-list.component';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ClientRoutingModule } from './client-routing.module';
import { MemberCardComponent } from '../member-card/member-card.component';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { PaginationComponent } from '../../../shared/pagination/pagination.component';
import { MemberDetailsComponent } from '../member-details/member-details.component';
import { MomentModule } from 'ngx-moment';
import { MemberPhotosComponent } from '../member-photos/member-photos.component';
@NgModule({
  declarations: [

    MembersListComponent,
    MemberCardComponent,
    MemberDetailsComponent,
    MemberPhotosComponent,
    PaginationComponent,

  ],
  imports: [CommonModule, 
    RouterModule,FormsModule,
    PaginationModule,ButtonsModule , ClientRoutingModule,MomentModule],
})
export class ClientModule {}
