import { HttpEvent, HttpHandlerFn, HttpRequest } from "@angular/common/http";
import { Observable, switchMap, catchError, throwError } from "rxjs";
import { AuthService } from "../../core/services/authService";
import { inject } from "@angular/core";

// export function isTokenExpired(token: string): boolean {
//     const decodedToken = JSON.parse(atob(token.split('.')[1])); // Decode the token payload
//     const expiryTime = decodedToken.exp * 1000; // Convert to milliseconds
//     return (Date.now() >= expiryTime);
// }

function addToken(request: HttpRequest<any>, accessToken: string | null): HttpRequest<any> {
    if (accessToken) {
        return request.clone({
            setHeaders: {
                Authorization: `Bearer ${accessToken}`
            }
        });
    }
    return request;
}

export function handle401Error(
    request: HttpRequest<any>,
    next: HttpHandlerFn
): Observable<HttpEvent<any>> {
    // Move the injection to inside the function
    const authService = inject(AuthService);

    return authService.refreshToken().pipe(
        switchMap(() => {
            const accessToken = localStorage.getItem('accessToken');
            return next(addToken(request, accessToken));
        }),
        catchError((error) => {
            console.error("Failed to refresh token:", error);
            authService.logout(); // Log out user or handle error as needed
            return throwError(() => error);
        })
    );
}
