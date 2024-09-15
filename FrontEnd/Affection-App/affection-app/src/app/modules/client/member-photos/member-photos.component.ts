import { ActivatedRoute } from '@angular/router';
import { MemberService } from './../../../core/services/member.service';
import { Component, inject, OnInit } from '@angular/core';

import { MemberPhotosResponse } from '../../../core/models/member/memberPhotosResponse';

@Component({
  selector: 'app-member-photos',
  templateUrl: './member-photos.component.html',
  styleUrl: './member-photos.component.css'
})
export class MemberPhotosComponent implements OnInit {


  photos: MemberPhotosResponse[] = [];
  memberService:MemberService = inject(MemberService);

  constructor(private activatedRoute:ActivatedRoute){}
  ngOnInit(): void {
    this.loadPhotos();
  }


  loadPhotos() {

    const userId = this.activatedRoute.snapshot.paramMap.get('id');
    this.memberService.getMemberPhotos(userId).subscribe((res) =>{

      this.photos = res
    })
  }
}
