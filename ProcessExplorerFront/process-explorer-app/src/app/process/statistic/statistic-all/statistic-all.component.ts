import { Component, OnInit } from '@angular/core';
import { IChartModel, ILoadingMember, IChartExtendedModel, IOsStatisticResponse } from 'src/app/shared/models/interfaces.models';
import { ProcessService } from '../../process.service';
import { IBestProcessesDay } from '../../models/process.models';
import { LoadingMemberService } from 'src/app/shared/services/loading-member.service';

@Component({
  selector: 'app-statistic-all',
  templateUrl: './statistic-all.component.html',
  styleUrls: ['./statistic-all.component.css']
})
export class StatisticAllComponent implements OnInit {

  //best day stats
  public bestDay: ILoadingMember<IBestProcessesDay>;

  //column chart stats
  public columnChart: ILoadingMember<IChartExtendedModel>;

  //pie chart stats -> os statistic
  public pieChart: ILoadingMember<IChartExtendedModel>;

  public maxNumOfSessions: number = 0;

  constructor(private service: ProcessService,
    private ls: LoadingMemberService) { }

  ngOnInit(): void {
    this.prepareCharts();
    this.getBestDay();
    this.loadPopularPeriodProcesses();
    this.loadOsStatistics();
  }

  prepareCharts(): void {
    this.bestDay = this.ls.createMember<IBestProcessesDay>();
    this.pieChart = this.ls.createChart(
      'pie',
      'Number of different processes (Last month period)',
      [{ backgroundColor: ['#320E3B', '#D8A47F'] }],
      true
    );
    this.columnChart = this.ls.createColumnChart(
      'Top 20 processes (Last month period)',
      '#F58F29'
    );
  }

  loadOsStatistics() {
    this.ls.handleChart<IOsStatisticResponse>(
      this.service.loadOsStatisticAll(),
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
      this.service.getDayWithMostProcesses(),
      this.bestDay
    );
  }

  loadPopularPeriodProcesses() {
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
