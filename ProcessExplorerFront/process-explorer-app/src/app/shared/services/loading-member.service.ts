import { Injectable } from '@angular/core';
import { ILoadingMember, IChartExtendedModel, IChartMapper } from 'src/app/shared/models/interfaces.models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoadingMemberService {

  constructor() { }

  handle<T>(method: Observable<T>, member: ILoadingMember<T>){
    method.subscribe((response: T) => {
        member.data = response;
        member.isLoading = false;
        member.errorMessage = null;
    },
    (error) => {
        member.isLoading = false;
        member.errorMessage = "Ann error occured";
    });
  }

  handleChart<T>(method: Observable<T>, member: ILoadingMember<IChartExtendedModel>, mapper: IChartMapper){
    method.subscribe((response: T) => {
        mapper.map<T>(response, member);
        member.isLoading = false;
        member.errorMessage = null;
    },
    (error) => {
        member.isLoading = false;
        member.errorMessage = "Ann error occured";
    });
  }

  createChart(type: string, mainTitle: string, colors: any[], charTitle: boolean = false): ILoadingMember<IChartExtendedModel> {
    return {
      data: {
        data: [],
        labels: [],
        title: charTitle,
        type: type,
        colors: colors,
        mainHeading: mainTitle
      },
      isLoading: true,
      errorMessage: null //string
    };
  }

  createColumnChart(mainTitle: string, color: string): ILoadingMember<IChartExtendedModel> {
    return {
      data: {
        data: [],
        labels: [],
        title: false,
        type: 'bar',
        colors: [{
          backgroundColor: color,
          borderColor: 'rgba(225,10,24,0.2)',
          pointBackgroundColor: 'rgba(225,10,24,0.2)',
          pointBorderColor: '#fff',
          pointHoverBackgroundColor: '#fff',
          pointHoverBorderColor: 'rgba(225,10,24,0.2)'
        }],
        mainHeading: mainTitle
      },
      isLoading: true,
      errorMessage: null
    }
  }

  createMember<T>(): ILoadingMember<T> {
    return {    
      data: null,
      isLoading: true,
      errorMessage: null 
    }
  }
}
