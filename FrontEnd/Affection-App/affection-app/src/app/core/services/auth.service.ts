
import { confirmEmailRequest } from './../models/authentication/confirmEmailRequest';
import { registerRequest } from './../models/authentication/registerRequest';
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Inject, Injectable, PLATFORM_ID } from "@angular/core";
import { BehaviorSubject, catchError, map, Observable, of, Subject, throwError } from 'rxjs';
import { Result } from '../models/result.model';
import { environment } from '../../../environments/environment';
import { Configurations } from '../../configs/config';
import { loginRequest } from '../models/authentication/loginRequest';
import { authResponse } from '../models/authentication/authResponse';
import { resendConfirmationEmailRequest } from '../models/authentication/resendConfirmationEmailRequest';
import { forgetPasswordRequest } from '../models/authentication/forgetPasswordRequest';
import { resetPasswordRequest } from '../models/authentication/resestPasswordRequest';
import { ErrorHandlingService } from './error-handling.service';
import { Router } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';


@Injectable({
    providedIn: 'root'
})

export class AuthService {

    private currentUserSource = new BehaviorSubject<authResponse | null>(null);
    currentUser$ = this.currentUserSource.asObservable();

    constructor(
        private http: HttpClient,
        private errorHandlingService: ErrorHandlingService,
        private router: Router,
        @Inject(PLATFORM_ID) private platformId: any

    ) {
        this.loadCurrentUser();

        if (isPlatformBrowser(this.platformId)) {
            localStorage.getItem('accessToken');
            localStorage.getItem('refreshToken');
        }
    }


    register(request: registerRequest): Observable<Result> {
        const url = `${environment.apiUrlAuth}${Configurations.Authentication.Register}`;
        return this.http.post<Result>(url, request).pipe(
            catchError(error => this.errorHandlingService.handleError(error))
        );
    }

    login(credential: loginRequest): Observable<authResponse> {
        const url = `${environment.apiUrlAuth}${Configurations.Authentication.Login}`;

        return this.http.post<authResponse>(url, credential)
            .pipe(
                map((res: authResponse) => {
                    localStorage.setItem('accessToken', res.token);
                    localStorage.setItem('refreshToken', res.refreshToken);
                    this.setCurrentUser(res);

                    return res;
                }),
                catchError(this.handleError)
            );
    }



    refreshToken(): Observable<authResponse> {
        const url = `${environment.apiUrlAuth}${Configurations.Authentication.GetRefreshToken}`;

        const token = this.getAccessToken();
        const refreshToken = this.getRefreshToken();




        return this.http.post<authResponse>(url, { token, refreshToken })
            .pipe(map(res => {
                localStorage.setItem('accessToken', res.token);
                localStorage.setItem('refreshToken', res.refreshToken);
                return res;
            }),

            );



    }



    confirmEmail(request: confirmEmailRequest): Observable<Result> {
        const url = `${environment.apiUrlAuth}${Configurations.Authentication.ConfirmEmail}`;
        return this.http.post<Result>(url, request).pipe(
            catchError(error => this.errorHandlingService.handleError(error))
        );
    }

    resendConfirmationEmail(request: resendConfirmationEmailRequest): Observable<Result> {
        const url = `${environment.apiUrlAuth}${Configurations.Authentication.ResendConfirmEmail}`;
        return this.http.post<Result>(url, request).pipe(
            catchError(error => this.errorHandlingService.handleError(error))
        );
    }

    sendResetPasswordCode(request: forgetPasswordRequest): Observable<Result> {
        const url = `${environment.apiUrlAuth}${Configurations.Authentication.ForgetPassword}`;
        return this.http.post<Result>(url, request).pipe(
            catchError(error => this.errorHandlingService.handleError(error))
        );
    }

    resetPassword(request: resetPasswordRequest): Observable<Result> {
        const url = `${environment.apiUrlAuth}${Configurations.Authentication.ResetPassword}`;
        return this.http.post<Result>(url, request).pipe(
            catchError(error => this.errorHandlingService.handleError(error))
        );
    }




    logout() {
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
        localStorage.removeItem('user');
        this.currentUserSource.next(null);
        this.router.navigate(['auth/login']);
    }

    isLoggedIn(): boolean {
        return localStorage.getItem('accessToken') !== null;
    }


    private getAccessToken() {
        return localStorage.getItem('accessToken');
    }

    private getRefreshToken() {
        return localStorage.getItem('refreshToken');
    }

    setCurrentUser(user: authResponse) {
        localStorage.setItem('user', JSON.stringify(user));
        this.currentUserSource.next(user);
    }

    private handleError(error: HttpErrorResponse): Observable<never> {
        // Custom error handling logic
        console.error('An error occurred:', error);
        return throwError(() => new Error(error.message || 'An unknown error occurred.'));
    }


    private loadCurrentUser() {
        if (typeof localStorage !== 'undefined') {
            const userJson = localStorage.getItem('user');
            if (userJson) {
                const user = JSON.parse(userJson);
                this.currentUserSource.next(user);
            }
        }
    }
}
