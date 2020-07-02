import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ICommunicationTypeResponse } from './models/admin.models';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) { }

  public getAll() {
    return this.http.get<ICommunicationTypeResponse>(`${environment.api}/communication/all`);
  }

  public changeType(type: number){
    return this.http.post(`${environment.api}/communication`, {type: type});
  }
}
