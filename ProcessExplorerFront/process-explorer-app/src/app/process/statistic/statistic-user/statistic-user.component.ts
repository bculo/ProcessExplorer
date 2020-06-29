import { Component, OnInit } from '@angular/core';
import { IChartModel, ILoadingMember } from 'src/app/shared/models/interfaces.models';
import { ProcessService } from '../../process.service';
import { IBestProcessesDay } from '../../models/process.models';

@Component({
  selector: 'app-statistic-user',
  templateUrl: './statistic-user.component.html',
  styleUrls: ['./statistic-user.component.css']
})
export class StatisticUserComponent implements OnInit {

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

    //column chart stats
    public lineChart: ILoadingMember<IChartModel> = {
      data: {
        data: [],
        labels: [],
        title: false,
        type: 'line',
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
    this.loadPopularUserProcesses();
    this.loadNumberOfProcessesPerSession();
  }

  getBestDay() {
    //TODO
  }

  loadNumberOfProcessesPerSession(){
    this.service.loadNumberOfProcessesPerSession()
    .subscribe((response) => {
        this.lineChart.data.data = response.chartRecords.value;
        this.lineChart.data.labels = response.chartRecords.label;
        
        this.lineChart.isLoading = false;
        this.lineChart.errorMessage = null;
      },
      (error) => {
        this.lineChart.isLoading = false;
        this.lineChart.errorMessage = "Ann error occured";
    });
  }

  loadPopularUserProcesses(){
    this.service.loadTopProcessesForChartUser()
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
