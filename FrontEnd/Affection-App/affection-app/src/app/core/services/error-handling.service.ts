import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class ErrorHandlingService {

  handleError(error: HttpErrorResponse) {
    let errorMessage = 'An error occurred. Please try again.';

    // Handle different HTTP error statuses
    switch (error.status) {
      case 400: // Bad Request
        errorMessage = 'The request was invalid. Please check your input and try again.';
        if (error.error.code === 'User.InvalidCode') {
          errorMessage = 'The code provided is invalid.';
        }
        break;

      case 401: // Unauthorized
        if (error.error.code === 'User.InvalidCredentials') {
          errorMessage = 'Invalid email or password.';
        } else if (error.error.code === 'User.EmailNotConfirmed') {
          errorMessage = 'Please confirm your email address.';
        } else if (error.error.code === 'User.InvalidJwtToken') {
          errorMessage = 'Your session has expired. Please log in again.';
        } else if (error.error.code === 'User.InvalidRefreshToken') {
          errorMessage = 'Your refresh token is invalid. Please log in again.';
        } else if (error.error.code === 'User.LockedUser') {
          errorMessage = 'Your account is locked. Please try again later.';
        } else if (error.error.code === 'User.RestrictedUser') {
          errorMessage = 'Your account has been restricted. Contact support for more details.';
        }
        break;

      case 403: // Forbidden
        errorMessage = 'You do not have permission to access this resource.';
        break;

      case 404: // Not Found
        errorMessage = 'The requested resource was not found.';
        break;

      case 409: // Conflict
        if (error.error?.code === 'User.DuplicatedEmail') {
          errorMessage = 'The email is already in use. Please choose a different one.';
        } else if (error.error?.code === 'User.DuplicatedConfirmedEmail') {
          errorMessage = 'The email is already confirmed.';
        }
        break;

      case 500: // Internal Server Error
        errorMessage = 'An internal server error occurred. Please try again later.';
        break;

      default:
        errorMessage = `An unexpected error occurred: ${error.message}`;
        break;
    }

    // Optionally use a user-friendly notification service instead of alert
    console.error('Error:', errorMessage); // Log the error message for debugging

    return throwError(() => new Error(errorMessage));
  }
}
