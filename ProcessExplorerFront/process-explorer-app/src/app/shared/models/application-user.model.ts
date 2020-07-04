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

    isValid(): boolean {
        if(!this.userName) return false;
        if(!this.userId) return false;
        if(!this._token) return false;
        return true;
    }
}