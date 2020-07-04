import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IBestApplicationDay, ITopApplications, IOpenedAppsPerSessionResponse, IApplicationItem } from './models/application.models';
import { environment } from 'src/environments/environment';
import { IOsStatisticResponse, IPaginationResponse } from '../shared/models/interfaces.models';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

  constructor(private http: HttpClient) { }

  private createSearchModel(searchCriteria: string, page: number): any {
    return {
      currentPage: page,
      searchCriteria: searchCriteria
    };
  }

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

  getNumberOfOpenedAppsPerSession(){
    return this.http.get<IOpenedAppsPerSessionResponse>(`${environment.api}/Application/openedappspersession`);
  }

  searchApplicationsPeriod(searchCriteria: string, page: number) {
    const request = this.createSearchModel(searchCriteria, page);
    return this.http.post<IPaginationResponse<IApplicationItem>>(`${environment.api}/Application/searchappsperiod`, request);
  }

  searchApplicationsUser(searchCriteria: string, page: number) {
    const request = this.createSearchModel(searchCriteria, page);
    return this.http.post<IPaginationResponse<IApplicationItem>>(`${environment.api}/Application/searchappsuser`, request);
  }
}
