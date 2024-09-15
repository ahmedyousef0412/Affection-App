import { MemberService } from './../../../core/services/member.service';
import { Component, inject, OnInit } from '@angular/core';
import { authResponse } from '../../../core/models/authentication/authResponse';
import { RequestFilter } from '../../../core/models/requestFilter';
import { PaginatedList } from '../../../core/models/pagination';
import { Member } from '../../../core/models/members';


@Component({
  selector: 'app-members-list',
  templateUrl: './members-list.component.html',
  styleUrl: './members-list.component.css'
})
export class MembersListComponent implements OnInit {


  user: authResponse;
  members: Member[];
  pagination: PaginatedList<Member>;
  userParams: RequestFilter;
  genderList = [{ value: 'Male', display: 'Males' }, { value: 'Female', display: 'Females' }]

  memberService: MemberService = inject(MemberService);

  constructor() {

    this.userParams = this.memberService.getUserParams();
  }

  ngOnInit(): void {

    this.loadMembers();
  }

  loadMembers() {

    this.memberService.getMembers().subscribe((res) => {

      console.log(res);

      this.members = res.items;
      this.pagination = {
        items: res.items,
        pageNumber: res.pageNumber,
        totalPages: res.totalPages,
        hasPreviousPage: res.hasPreviousPage,
        hasNextPage: res.hasNextPage
      }
    })
  }



  pageChanged(pageNumber: number) {
    this.userParams.pageNumber = pageNumber;
    this.memberService.setUserParams(this.userParams);
    this.loadMembers();
  }

  resetFilters() {
    this.userParams = this.memberService.resetUserParams();
    this.loadMembers();
  }
}
