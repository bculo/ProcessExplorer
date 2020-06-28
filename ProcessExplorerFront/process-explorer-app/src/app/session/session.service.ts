import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ISessionStatsResponse } from './models/session.models';

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  constructor(private http: HttpClient) { }

  getSessionStatistic(){
    return this.http.get<ISessionStatsResponse>(`${environment.api}/Session/sessionstatistic`);
  }
}
