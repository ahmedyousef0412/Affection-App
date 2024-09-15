import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { UserProfile } from '../models/account/userProfile';
import { environment } from '../../../environments/environment';
import { Configurations } from '../../configs/config';
import { ErrorHandlingService } from './error-handling.service';
import { EditProfileRequest } from '../models/account/updateProfile';
import { Result } from '../models/result.model';
import { ChangePasswordRequest } from '../models/account/changePassword';
import { PhotoResponse } from '../models/account/photoResponse';

@Injectable({
    providedIn: 'root',
})
export class AccountService {
    constructor(
        private httpClient: HttpClient,
        private errorHandlingService: ErrorHandlingService
    ) { }

    //Get User Profile
    userProfile(): Observable<UserProfile> {
        const url = `${environment.apiUrlAuth}${Configurations.Account.Profile}`;

        return this.httpClient
            .get<UserProfile>(url)
            .pipe(
                catchError((error) => this.errorHandlingService.handleError(error))
            );
    }

    //Update User Profile
    updateProfile(request: EditProfileRequest) {
        const url = `${environment.apiUrlAuth}${Configurations.Account.UpdateProfile}`;

        return this.httpClient.put<Result>(url, request).pipe(
            catchError((error) => this.errorHandlingService.handleError(error))

        );

    }

    changePassword(request: ChangePasswordRequest) {
        const url = `${environment.apiUrlAuth}${Configurations.Account.ChangePassword}`;

        return this.httpClient.put<Result>(url, request).pipe(
            catchError((error) => this.errorHandlingService.handleError(error))

        );
    }


    getPhotos(): Observable<PhotoResponse[]> {
        const url = `${environment.apiUrlAuth}${Configurations.Account.ShowPhotos}`;

        return this.httpClient.get<PhotoResponse[]>(url);
    }

    setPhotoMain(photoId: number) {
        const url = `${environment.apiUrlAuth}${Configurations.Account.SetPhotoMain.replace('{photoId}', photoId.toString())}`;

        return this.httpClient.put<Result>(url, {}).pipe(
            catchError((error) => this.errorHandlingService.handleError(error))
        );
    }

    deletePhoto(photoId: number) {
        const url = `${environment.apiUrlAuth}${Configurations.Account.DeletePhoto.replace('{photoId}', photoId.toString())}`;

        return this.httpClient.delete<Result>(url, {}).pipe(
            catchError((error) => this.errorHandlingService.handleError(error))
        );
    }

}
