import { authResponse } from './authentication/authResponse';

export class RequestFilter {
    gender: string;
    minAge: number = 18;
    maxAge: number = 60;
    pageNumber: number = 1;
    pageSize: number = 5;
    orderBy: string = 'lastActive';
    constructor(user: authResponse) {
        // Set gender filter based on the logged-in user's gender
        this.gender = user.gender === 'Female' ? 'Male' : 'Female';
    }
}
