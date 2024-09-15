import { Component, inject, OnInit } from '@angular/core';
import { MemberService } from '../../../core/services/member.service';
import { ActivatedRoute } from '@angular/router';
import { MemberResponse } from '../../../core/models/member/memberResponse';

@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrl: './member-details.component.css'
})
export class MemberDetailsComponent implements OnInit {
  
  
  
  
  memberService:MemberService= inject(MemberService);
  member: MemberResponse = {} as MemberResponse;
  
  constructor(private activatedRoute: ActivatedRoute){}
  
  ngOnInit(): void {
    this.getMember()
  }


  getMember(){
   this.memberService.getMember(this.activatedRoute.snapshot.paramMap.get('id')).subscribe((member) =>{
    this.member = member;
    console.log(this.activatedRoute.snapshot.paramMap.get('id'));
    
   })
  }


}
