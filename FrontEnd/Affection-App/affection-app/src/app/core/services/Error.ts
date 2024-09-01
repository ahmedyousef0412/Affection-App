export interface Errors {
    type?: string;
    title?: string;
    status?: number;
    errors?: string[]; // Array of error messages
}
