
import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { AuthService } from '../services/authService';
import { inject, PLATFORM_ID } from '@angular/core';
import { Router } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';
import { catchError, throwError } from 'rxjs';
import { error } from 'console';



export const authInterceptor: HttpInterceptorFn = (req, next) => {
    const authService = inject(AuthService);
    const router = inject(Router);
    const platformId = inject(PLATFORM_ID);

    if (isPlatformBrowser(platformId)) {
        const isLoggedIn = authService.isLoggedIn();

        if (isLoggedIn) {
            req = req.clone({
                setHeaders: {
                  Authorization: `Bearer ${localStorage.getItem('accessToken')}`
                }
            });
        }
        
    }

    return next(req);
    // return next(req).pipe(
        
    //     catchError((error: HttpErrorResponse) => {
            
    //         if(error.status === 401){
    //             const isRefresh = confirm("The session is expire, Do you want to continue");

    //             if(isRefresh){
    //                 authService.$refreshToken.next(true);
    //             }
    //         }
    //         return throwError(error)

    //     })
    // );
};