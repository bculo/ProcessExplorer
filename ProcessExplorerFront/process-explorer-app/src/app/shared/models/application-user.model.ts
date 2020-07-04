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
        if(this.userName === null) return false;
        if(this.userId === null) return false;
        if(this._token === null) return false;
        return true;
    }
}