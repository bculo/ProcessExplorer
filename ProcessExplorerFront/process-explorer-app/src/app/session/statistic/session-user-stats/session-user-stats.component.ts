import { Component, OnInit } from '@angular/core';
import { IChartModel } from 'src/app/shared/models/interfaces.models';
import { SessionService } from '../../session.service';
import { ISessionStatsResponse, IPieChartRecords, IActivityRecords } from '../../models/session.models';

@Component({
  selector: 'app-session-user-stats',
  templateUrl: './session-user-stats.component.html',
  styleUrls: ['./session-user-stats.component.css']
})
export class SessionUserStatsComponent implements OnInit {

  public errorMessage: string | null = null;
  public isLoading: boolean = true;

  //pie chart stats
  public pieChart: IChartModel = {
    data: [],
    labels: [],
    title: true,
    type: 'doughnut',
    colors: [{ backgroundColor:['#FF7360', '#6FC8CE'] }],
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
    colors: [{ backgroundColor: ['#0F7173'] }],
  }

  constructor(public service: SessionService) { }

  ngOnInit(): void {
    this.service.getSessionStatisticUser()
      .subscribe(response => {
        this.setPieChartValues(response.pieChartRecords);
        this.setNumberStatistics(response);
        this.setRecordsForActivityChart(response.activityChartRecords);

        this.isLoading = false;
        this.errorMessage = null;
      },
      (error) => {
        this.isLoading = false;
        this.errorMessage = "Ann error occurred";
      });
  }

  setPieChartValues(records: IPieChartRecords){
    this.pieChart.data = records.quantity;
    this.pieChart.labels = records.name;
  }

  setNumberStatistics(response: ISessionStatsResponse) {
    this.numberStats.mostActiveDay = response.mostActiveDay?.date;
    this.numberStats.maxNumberOfSession = response.mostActiveDay?.numberOfSessions;
  }

  setRecordsForActivityChart(records: IActivityRecords) {
    this.activityChart.data = records.number;
    this.activityChart.labels = records.date;
  }
}
