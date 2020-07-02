/*
-----------------------
|       REQUESTS      |
-----------------------
*/

export class LoginRequestModel {
    constructor(public identifier: string, 
        public password: string,
        public isWebApp) {}
}

export class RegisterRequestModel {
    constructor(public username: string,
        public email: string, 
        public password: string) {}
}

/*
-----------------------
|      RESPONSES      |
-----------------------
*/

export interface ILoginResponse {
    userId: string;
    userName: string;
    expireIn: string;
    jwtToken: string;
}