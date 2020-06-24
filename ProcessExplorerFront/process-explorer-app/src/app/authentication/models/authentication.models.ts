/*
-----------------------
|       REQUESTS      |
-----------------------
*/

export class LoginRequestModel {
    constructor(public identifier: string, 
        public password: string) {}
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
    id: string;
    username: string;
    expiresIn: string;
    jwt: string;
}