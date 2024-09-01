
export interface authResponse {
   
    id: string;
    userName?: string;
    email?: string;
    knowAs: string;
    country: string;
    city: string;
    gender: string;
    dateOfBirth: Date;
    token: string;
    expiresIn: number;
    refreshToken: string;
    refreshTokenExpiration: Date;
    photoUrl?: string;
    emailConfirmed?: boolean; 
}