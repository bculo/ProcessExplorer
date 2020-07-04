import { Component, OnInit } from '@angular/core';
import { IChartModel, ILoadingMember, IChartExtendedModel } from 'src/app/shared/models/interfaces.models';
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
  public columnChart: ILoadingMember<IChartExtendedModel> = {
    data: {
      data: [],
      labels: [],
      title: false,
      type: 'bar',
      colors: [{
        backgroundColor: '#F58F29',
        borderColor: 'rgba(225,10,24,0.2)',
        pointBackgroundColor: 'rgba(225,10,24,0.2)',
        pointBorderColor: '#fff',
        pointHoverBackgroundColor: '#fff',
        pointHoverBorderColor: 'rgba(225,10,24,0.2)'
      }],
      mainHeading: 'Top 20 processes (Last month period)'
    },
    isLoading: true,
    errorMessage: null //string
  }

  //pie chart stats -> os statistic
  public pieChart: ILoadingMember<IChartExtendedModel> = {
    data: {
      data: [],
      labels: [],
      title: true,
      type: 'pie',
      colors: [{ backgroundColor:[ '#320E3B', '#D8A47F'] }],
      mainHeading: 'Number of different processes (Last month period)'
    },
    isLoading: true,
    errorMessage: null //string
  }

  public maxNumOfSessions: number = 0;

  constructor(private service: ProcessService) { }

  ngOnInit(): void {
    this.getBestDay();
    this.loadPopularPeriodProcesses();
    this.loadOsStatistics();
  }

  loadOsStatistics(){
    this.service.loadOsStatisticAll()
      .subscribe((response) => {
        this.pieChart.data.data = response.pieChart.quantity;
        this.pieChart.data.labels = response.pieChart.name;
        
        this.pieChart.isLoading = false;
        this.pieChart.errorMessage = null;
      },
      (error) => {
        this.pieChart.isLoading = false;
        this.pieChart.errorMessage = "Ann error occured";
      });
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
