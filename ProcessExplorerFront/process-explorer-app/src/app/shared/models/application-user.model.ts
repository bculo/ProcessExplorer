export class ApplicationUser {
    constructor(public userName: string, 
        public userId: string, 
        private _token: string, 
        private _tokenExpirationDate: Date) {}

    get token() {
        if(!this._tokenExpirationDate || new Date() > this._tokenExpirationDate)
            return null;
        return this._token;
    }
}