

export interface Result<T = void> {
    isSuccess: boolean;
    error: Error;
    value?: T;
  }

  export interface Error {
    code: string;
    message: string;
  }