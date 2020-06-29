import { Component, OnInit } from '@angular/core';
import { IChartModel, ILoadingMember } from 'src/app/shared/models/interfaces.models';
import { ProcessService } from '../../process.service';
import { IBestProcessesDay } from '../../models/process.models';

@Component({
  selector: 'app-statistic-all',
  templateUrl: './statistic-all.component.html',
  styleUrls: ['./statistic-all.component.css']
})
export class StatisticAllComponent implements OnInit {

  //best day stats
  public bestDay: ILoadingMember<IBestProcessesDay> = {
    data: null, //where data IBestProcessesDay
    isLoading: true,
    errorMessage: null //string
  }

  //column chart stats
  public columnChart: ILoadingMember<IChartModel> = {
    data: {
      data: [],
      labels: [],
      title: false,
      type: 'bar',
      colors: [{
        backgroundColor: '#FF7360',
        borderColor: 'rgba(225,10,24,0.2)',
        pointBackgroundColor: 'rgba(225,10,24,0.2)',
        pointBorderColor: '#fff',
        pointHoverBackgroundColor: '#fff',
        pointHoverBorderColor: 'rgba(225,10,24,0.2)'
      }],
    },
    isLoading: true,
    errorMessage: null //string
  }

  public maxNumOfSessions: number = 0;

  constructor(private service: ProcessService) { }

  ngOnInit(): void {
    this.getBestDay();
    this.loadPopularPeriodProcesses();
  }

  getBestDay() {
    this.service.getDayWithMostProcesses()
      .subscribe((response) => {
        this.bestDay.data = response;
        this.bestDay.isLoading = false;
        this.bestDay.errorMessage = null;
      },
      (error) => {
        this.bestDay.isLoading = false;
        this.bestDay.errorMessage = "Ann error occured";
      });
  }

  loadPopularPeriodProcesses(){
    this.service.loadTopProcessesForChartAllUsers()
      .subscribe((response) => {
        this.columnChart.data.data = response.chartRecords.value;
        this.columnChart.data.labels = response.chartRecords.label;
        this.maxNumOfSessions = response.maxNumberOfSessions;
        
        this.columnChart.isLoading = false;
        this.columnChart.errorMessage = null;
      },
      (error) => {
        this.columnChart.isLoading = false;
        this.columnChart.errorMessage = "Ann error occured";
      });
  }

}
