import { Member } from './../models/members';
import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { RequestFilter } from '../models/requestFilter';
import { catchError, map, of, take } from 'rxjs';
import { PaginatedList } from '../models/pagination';

import { AuthService } from './auth.service';
import { authResponse } from '../models/authentication/authResponse';
import { environment } from '../../../environments/environment';
import { Configurations } from '../../configs/config';
import { ErrorHandlingService } from './error-handling.service';
import { MembersResponse } from '../models/member/members';
import { MemberPhotosResponse } from '../models/member/memberPhotosResponse';


@Injectable({
  providedIn: 'root',
})
export class MemberService {
  membersCached = new Map();
  memberPhotosCached = new Map();
  user: authResponse;
  filters: RequestFilter;
  authService: AuthService = inject(AuthService);

  constructor(
    private httpClient: HttpClient,
    private errorHandlingService: ErrorHandlingService
  ) {
    this.authService.currentUser$.pipe(take(1)).subscribe((user) => {
      this.user = user;

      this.filters = new RequestFilter(user);
    });
  }

  //Get Members

  getMembers() {
    const url = `${environment.apiUrl}${Configurations.Clients.GetMembers}`;

    const cacheKey = Object.values(this.filters).join('-');

    // Check if the response is cached
    const cachedResponse = this.membersCached.get(cacheKey);
    if (cachedResponse) {
      return of(cachedResponse);
    }

    let params = this.getPagination(
      this.filters.pageNumber,
      this.filters.pageSize
    );

    params = params.append('minAge', this.filters.minAge.toString());
    params = params.append('maxAge', this.filters.maxAge.toString());
    params = params.append('gender', this.filters.gender);
    params = params.append('orderBy', this.filters.orderBy);

    return this.httpClient.get<PaginatedList<Member>>(url, { params }).pipe(
      map((response) => {
        this.membersCached.set(cacheKey, response);
        return response;
      }),
      catchError((error) => {
        return this.errorHandlingService.handleError(error);
      })
    );
  }

  getMember(id:string){
    const url = `${environment.apiUrl}${Configurations.Clients.GetMember.replace('{id}', id)}`;

    return this.httpClient.get<MembersResponse>(url).pipe(
      catchError(error => this.errorHandlingService.handleError(error))
    )

  }

  getMemberPhotos(id: string) {
    console.log('getMemberPhotos called with id:', id);
    const url = `${environment.apiUrl}${Configurations.Clients.GetMemberPhotos.replace('{id}', id)}`;
    const cacheKey = `memberPhotos_${id}`;
    
    // Check cache
    const cachedResponse = this.memberPhotosCached.get(cacheKey);
    if (cachedResponse) {
      console.log('Cache hit:', cacheKey);
      console.log('Cached response:', cachedResponse);
      return of(cachedResponse);
    }
  
    // Fetch from server
    return this.httpClient.get<MemberPhotosResponse[]>(url).pipe(
      map((response) => {
        console.log('Server response:', response);
        this.memberPhotosCached.set(cacheKey, response);
        return response;
      }),
      catchError((error) => {
        console.error('Error occurred:', error);
        return this.errorHandlingService.handleError(error);
      })
    );
  }
  

  getUserParams() {
    return this.filters;
  }

  setUserParams(filters: RequestFilter) {
    this.filters = filters;
  }

  resetUserParams() {
    this.filters = new RequestFilter(this.user);
    return this.filters;
  }

 
  private getPagination(pageNumber: number, pageSize: number) {
    let params = new HttpParams();

    const validPageNumber = pageNumber ?? 1; 
    const validPageSize = pageSize ?? 5
    ; 
    params = params.append('pageNumber', validPageNumber.toString());
    params = params.append('pageSize', validPageSize.toString());

    return params;
  }
}
