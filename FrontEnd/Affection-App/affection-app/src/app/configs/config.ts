import { environment } from "../../environments/environment";

export abstract class Configurations {


    static readonly ApiBaseUrl: string = `${environment.apiUrlAuth}`;

    //Object.freeze() ensures that the Authentication object is immutable, preventing accidental modifications.

    static readonly Authentication = Object.freeze({
        Register: 'auth/register',
        Login: 'auth/login',
        ConfirmEmail: 'auth/confirm-email',
        ResendConfirmEmail: 'auth/resend-confirm-email',
        ForgetPassword: 'auth/forget-password',
        ResetPassword: 'auth/reset-password',
        GetRefreshToken: 'auth/refresh-token'
    });
   
    static readonly Clients = Object.freeze({
        Members: 'members',

    });


    static getFullPath(endpoint: string): string {
        return `${this.ApiBaseUrl}${endpoint}`;
    }
}