import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IProcessPaginationResponseDto, IProcessItem } from './models/process.models';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProcessService {

  constructor(private http: HttpClient) { }

  searchUserProcesses(searchCriteria: string, page: number){

    const request = {
      currentPage: page,
      searchCriteria: searchCriteria
    };

    return this.http.post<IProcessPaginationResponseDto>(`${environment.api}/Process/searchforuser`, request);
  }

  
  searchAllProcesses(searchCriteria: string, page: number){

    const request = {
      currentPage: page,
      searchCriteria: searchCriteria
    };

    return this.http.post<IProcessPaginationResponseDto>(`${environment.api}/Process/searchall`, request);
  }
}
