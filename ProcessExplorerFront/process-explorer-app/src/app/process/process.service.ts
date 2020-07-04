import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IProcessPaginationResponseDto, IBestProcessesDay, ITopProcessesPeriodResponseDto } from './models/process.models';
import { IOsStatisticResponse } from '../shared/models/interfaces.models';

@Injectable({
  providedIn: 'root'
})
export class ProcessService {

  constructor(private http: HttpClient) { }

  searchUserProcesses(searchCriteria: string, page: number){
    const request = this.createSearchModel(searchCriteria, page);
    return this.http.post<IProcessPaginationResponseDto>(`${environment.api}/Process/searchforuser`, request);
  }

  searchAllProcesses(searchCriteria: string, page: number){
    const request = this.createSearchModel(searchCriteria, page);
    return this.http.post<IProcessPaginationResponseDto>(`${environment.api}/Process/searchall`, request);
  }

  private createSearchModel(searchCriteria: string, page: number): any {
    return {
      currentPage: page,
      searchCriteria: searchCriteria
    };
  }

  getDayWithMostProcesses() {
    return this.http.get<IBestProcessesDay>(`${environment.api}/Process/mostprocesses`);
  }

  getDayWithMostProcessesUser() {
    return this.http.get<IBestProcessesDay>(`${environment.api}/Process/mostprocessesuser`);
  }

  loadTopProcessesForChartAllUsers() {
    return this.http.get<ITopProcessesPeriodResponseDto>(`${environment.api}/Process/topprocessesperiod`);
  }

  loadTopProcessesForChartUser() {
    return this.http.get<ITopProcessesPeriodResponseDto>(`${environment.api}/Process/topprocessesuser`);
  }

  loadNumberOfProcessesPerSession(){
    return this.http.get<ITopProcessesPeriodResponseDto>(`${environment.api}/Process/processstatsforsessions`);
  }

  loadOsStatisticAll(){
    return this.http.get<IOsStatisticResponse>(`${environment.api}/Process/osstatisticperiod`);
  }

  loadOsStatisticUser(){
    return this.http.get<IOsStatisticResponse>(`${environment.api}/Process/osstatisticperioduser`);
  }
}
