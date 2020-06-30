import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ISessionStatsResponse, ISessionItem, ISingleSession, ISessionProcessItem } from './models/session.models';
import { IPaginationResponse } from '../shared/models/interfaces.models';

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  public choosenSession: any = {
    sessionId: ""
  };

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

  getSelectedSession(sessionId: string) {
    return this.http.post<ISingleSession>(`${environment.api}/Session/getsession`, { selectedSessionId: sessionId });
  }

  getSessionProcesses(sessionId: string, currentPage: number){
    return this.http.post<IPaginationResponse<ISessionProcessItem>>(`${environment.api}/Process/searchchoosensession`, { sessionId: sessionId, currentPage: currentPage });
  }

}
