import { Component, OnInit } from '@angular/core';
import {
  IChartModel,
  ILoadingMember,
  IChartExtendedModel,
  IOsStatisticResponse,
} from 'src/app/shared/models/interfaces.models';
import { ProcessService } from '../../process.service';
import { IBestProcessesDay, ITopProcessesPeriodResponseDto } from '../../models/process.models';
import { LoadingMemberService } from 'src/app/shared/services/loading-member.service';

@Component({
  selector: 'app-statistic-user',
  templateUrl: './statistic-user.component.html',
  styleUrls: ['./statistic-user.component.css'],
})
export class StatisticUserComponent implements OnInit {
  
  //best day stats
  public bestDay: ILoadingMember<IBestProcessesDay>;

  //column chart stats -> TOP PROCESSES ALL TIME
  public columnChart: ILoadingMember<IChartExtendedModel>;

  //line chart stats
  public lineChart: ILoadingMember<IChartExtendedModel>;

  //pie chart stats -> os statistic
  public pieChart: ILoadingMember<IChartExtendedModel>;

  public maxNumOfSessions: number = 0;

  constructor(
    private service: ProcessService,
    private ls: LoadingMemberService
  ) {}

  ngOnInit(): void {
    this.prepareCharts();
    this.getBestDay();
    this.loadPopularUserProcesses();
    this.loadNumberOfProcessesPerSession();
    this.loadOsStatistics();
  }

  prepareCharts(): void {
    this.bestDay = this.ls.createMember<IBestProcessesDay>();
    this.pieChart = this.ls.createChart(
      'doughnut',
      'Operating system statistics (Last month period)',
      [{ backgroundColor: ['#A14A76', '#CDB2AB'] }],
      true
    );
    this.lineChart = this.ls.createChart(
      'line',
      'Number of different processes for each session',
      [{ backgroundColor: ['#FBC2B5'] }]
    );
    this.columnChart = this.ls.createColumnChart(
      'Top 10 processes for user (All time)',
      '#4464AD'
    );
  }

  loadOsStatistics() {
    this.ls.handleChart<IOsStatisticResponse>(
      this.service.loadOsStatisticUser(),
      this.pieChart,
      {
        map<IOsStatisticResponse>(response, member) {
          member.data.data = response.pieChart.quantity;
          member.data.labels = response.pieChart.name;
        },
      }
    );
  }

  getBestDay() {
    this.ls.handle<IBestProcessesDay>(
      this.service.getDayWithMostProcessesUser(),
      this.bestDay
    );
  }

  loadNumberOfProcessesPerSession() {
    this.ls.handleChart<ITopProcessesPeriodResponseDto>(
      this.service.loadNumberOfProcessesPerSession(),
      this.lineChart,
      {
        map<ITopProcessesPeriodResponseDto>(response, member) {
          member.data.data = response.chartRecords.value;
          member.data.labels = response.chartRecords.label;
        },
      }
    );
  }

  loadPopularUserProcesses() {
    this.service.loadTopProcessesForChartUser().subscribe(
      (response) => {
        this.columnChart.data.data = response.chartRecords.value;
        this.columnChart.data.labels = response.chartRecords.label;
        this.maxNumOfSessions = response.maxNumberOfSessions;

        this.columnChart.isLoading = false;
        this.columnChart.errorMessage = null;
      },
      (error) => {
        this.columnChart.isLoading = false;
        this.columnChart.errorMessage = 'Ann error occured';
      }
    );
  }
}
