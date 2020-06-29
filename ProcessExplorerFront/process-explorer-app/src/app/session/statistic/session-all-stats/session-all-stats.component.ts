import { Component, OnInit } from '@angular/core';
import { SessionService } from '../../session.service';
import { IActivityRecords, ISessionStatsResponse, IPieChartRecords } from '../../models/session.models';
import { IChartModel } from 'src/app/shared/models/interfaces.models';

@Component({
  selector: 'app-session-all-stats',
  templateUrl: './session-all-stats.component.html',
  styleUrls: ['./session-all-stats.component.css']
})
export class SessionAllStatsComponent implements OnInit {

  public errorMessage: string | null = null;
  public isLoading: boolean = true;

  //pie chart stats
  public pieChart: IChartModel = {
    data: [],
    labels: [],
    title: true,
    type: 'doughnut',
    colors: [{ backgroundColor:['#FF7360', '#6FC8CE', '#FAFFF2', '#FFFCC4', '#B9E8E0'] }],
  }

  //number statistic
  public numberStats: any = {
    mostActiveDay: "",
    maxNumberOfSession: 0,
    totalNumberOfSessions: 0,
    numberOfUsers: 0
  };

  //settings for activity chart
  public activityChart: IChartModel = {
    data: [],
    labels: [],
    title: false,
    type: 'line',
    colors: [{ backgroundColor: ['#FF7360'] }],
  }

  constructor(public service: SessionService) { }

  ngOnInit(): void {
    this.service.getSessionStatisticAll()
      .subscribe(response => {
        this.setPieChartValues(response.pieChartRecords);
        this.setNumberStatistics(response);
        this.setRecordsForActivityChart(response.activityChartRecords);

        this.isLoading = false;
        this.errorMessage = null;
      },
      (error) => {
        this.errorMessage = "An error occurred";
        this.isLoading = false;
      });
  }

  setPieChartValues(records: IPieChartRecords){
    this.pieChart.data = records.quantity;
    this.pieChart.labels = records.name;
  }

  setNumberStatistics(response: ISessionStatsResponse) {
    this.numberStats.mostActiveDay = response.mostActiveDay.date;
    this.numberStats.maxNumberOfSession = response.mostActiveDay.numberOfSessions;
    this.numberStats.totalNumberOfSessions = response.totalNumberOfSessions;
    this.numberStats.numberOfUsers = response.numberOfUsers;
  }

  setRecordsForActivityChart(records: IActivityRecords) {
    this.activityChart.data = records.number;
    this.activityChart.labels = records.date;
  }
}
