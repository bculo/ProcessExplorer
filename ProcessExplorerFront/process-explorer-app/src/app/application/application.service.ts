import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IBestApplicationDay, ITopApplications } from './models/application.models';
import { environment } from 'src/environments/environment';
import { IOsStatisticResponse } from '../shared/models/interfaces.models';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

  constructor(private http: HttpClient) { }

  getDayWithMostOpenedApplicaitonAllUsers() {
    return this.http.get<IBestApplicationDay>(`${environment.api}/Application/daywithmostappsperiod`);
  }

  getDayWithMostOpenedApplicaitonSingleUser() {
    return this.http.get<IBestApplicationDay>(`${environment.api}/Application/daywithmostappsuser`);
  }

  getOsStatisticAllPeriod(){
    return this.http.get<IOsStatisticResponse>(`${environment.api}/Application/osappstatisticperiod`);
  }

  getOsStatisticUserPeriod(){
    return this.http.get<IOsStatisticResponse>(`${environment.api}/Application/osappstatisticuser`);
  }

  getTopOpenedAppsPeriod(){
    return this.http.get<ITopApplications>(`${environment.api}/Application/topopenedappsperiod`);
  }

  getTopOpenedAppsUserPeriod(){
    return this.http.get<ITopApplications>(`${environment.api}/Application/topopenedappsuser`);
  }

  getTopOpenedAppsForChoosenSession(sessionId: string){
    return this.http.post<ITopApplications>(`${environment.api}/Application/osappstatisticuser`, { sessionId: sessionId });
  }
}
