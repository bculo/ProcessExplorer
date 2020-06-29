import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ISessionStatsResponse, ISessionItem } from './models/session.models';
import { IPaginationResponse } from '../shared/models/interfaces.models';

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  constructor(private http: HttpClient) { }

  getSessionStatisticAll(){
    return this.http.get<ISessionStatsResponse>(`${environment.api}/Session/sessionstatistic`);
  }

  getSessionStatisticUser(){
    return this.http.get<ISessionStatsResponse>(`${environment.api}/Session/usersessionstatistic`);
  }

  getSessionsForUsers(currentPage: number){
    return this.http.post<IPaginationResponse<ISessionItem>>(`${environment.api}/Session/userlistsessions`, { currentPage: currentPage });
  }


}
