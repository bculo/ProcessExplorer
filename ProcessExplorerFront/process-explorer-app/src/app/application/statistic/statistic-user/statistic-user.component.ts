import { Component, OnInit } from '@angular/core';
import { ILoadingMember, IChartExtendedModel } from 'src/app/shared/models/interfaces.models';
import { IBestApplicationDay } from '../../models/application.models';
import { ApplicationService } from '../../application.service';

@Component({
  selector: 'app-statistic-user',
  templateUrl: './statistic-user.component.html',
  styleUrls: ['./statistic-user.component.css']
})
export class StatisticUserComponent implements OnInit {

  //best day stats
  public bestDay: ILoadingMember<IBestApplicationDay> = {
    data: null, //where data IBestApplicationDay
    isLoading: true,
    errorMessage: null //string
  }

  //column chart stats -> TOP 10 applications
  public columnChart: ILoadingMember<IChartExtendedModel> = {
    data: {
      data: [],
      labels: [],
      title: false,
      type: 'bar',
      colors: [{
        backgroundColor: '#FF6663',
        borderColor: 'rgba(225,10,24,0.2)',
        pointBackgroundColor: 'rgba(225,10,24,0.2)',
        pointBorderColor: '#fff',
        pointHoverBackgroundColor: '#fff',
        pointHoverBorderColor: 'rgba(225,10,24,0.2)'
      }],
      mainHeading: 'Top 10 applications (Last month period)'
    },
    isLoading: true,
    errorMessage: null //string
  }

  //pie chart stats -> os statistics
  public pieChart: ILoadingMember<IChartExtendedModel> = {
    data: {
      data: [],
      labels: [],
      title: true,
      type: 'doughnut',
      colors: [{ backgroundColor:[ '#E0FF4F', '#00272B'] }],
      mainHeading: 'Number of opened applications (Last month period)'
    },
    isLoading: true,
    errorMessage: null,
  }

  //Number of oppened apps per session
  public lineChart: ILoadingMember<IChartExtendedModel> = {
    data: {
      data: [],
      labels: [],
      title: false,
      type: 'line',
      colors: [{ backgroundColor: ['#95C623'] }],
      mainHeading: 'Number of oppened apps per session (Last 20 sessions)'
    },
    isLoading: true,
    errorMessage: null //string
  }

  public maxNumOfSessions: number = 0;

  constructor(private service: ApplicationService) { }

  ngOnInit(): void {
    this.getBestDayAllUsers();
    this.loadOsStatisticsPeriod();
    this.getTopOpenedAppsPeriod();
    this.getOpenedAppsPerSession();
  }

  getBestDayAllUsers() {
    this.service.getDayWithMostOpenedApplicaitonSingleUser()
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

  loadOsStatisticsPeriod() {
      this.service.getOsStatisticUserPeriod()
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

  getTopOpenedAppsPeriod(){
    this.service.getTopOpenedAppsUserPeriod()
    .subscribe((response) => {
      this.columnChart.data.data = response.chartRecords.value;
      this.columnChart.data.labels = response.chartRecords.label;
      
      this.columnChart.isLoading = false;
      this.columnChart.errorMessage = null;
    },
    (error) => {
      this.columnChart.isLoading = false;
      this.columnChart.errorMessage = "Ann error occured";
    });
  }

  getOpenedAppsPerSession(){
    this.service.getNumberOfOpenedAppsPerSession()
    .subscribe((response) => {
      this.lineChart.data.data = response.chart.number;
      this.lineChart.data.labels = response.chart.date;
      
      this.lineChart.isLoading = false;
      this.lineChart.errorMessage = null;
    },
    (error) => {
      this.lineChart.isLoading = false;
      this.lineChart.errorMessage = "Ann error occured";
    });
  }


}
