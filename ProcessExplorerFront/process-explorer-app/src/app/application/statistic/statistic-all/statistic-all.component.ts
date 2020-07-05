import { Component, OnInit } from '@angular/core';
import {
  ILoadingMember,
  IChartExtendedModel,
  IOsStatisticResponse,
} from 'src/app/shared/models/interfaces.models';
import { ApplicationService } from '../../application.service';
import {
  IBestApplicationDay,
  ITopApplications,
} from '../../models/application.models';
import { LoadingMemberService } from 'src/app/shared/services/loading-member.service';

@Component({
  selector: 'app-statistic-all',
  templateUrl: './statistic-all.component.html',
  styleUrls: ['./statistic-all.component.css'],
})
export class StatisticAllComponent implements OnInit {
  //best day stats
  public bestDay: ILoadingMember<IBestApplicationDay>;

  //column chart stats -> TOP 20 apps
  public columnChart: ILoadingMember<IChartExtendedModel>;

  //os statistics
  public pieChart: ILoadingMember<IChartExtendedModel>;

  constructor(
    private service: ApplicationService,
    private ls: LoadingMemberService
  ) {}

  ngOnInit(): void {
    this.prepareCharts();
    this.getBestDayAllUsers();
    this.loadOsStatisticsPeriod();
    this.getTopOpenedAppsPeriod();
  }

  prepareCharts(): void {
    this.bestDay = this.ls.createMember<IBestApplicationDay>();
    this.pieChart = this.ls.createChart(
      'doughnut',
      'Number of opened applications (Last month period)',
      [{ backgroundColor: ['#23B5D3', '#EA526F'] }],
      true
    );
    this.columnChart = this.ls.createColumnChart(
      'Top 20 opened applications (Last month period)',
      '#A51080'
    );
  }

  getBestDayAllUsers() {
    this.ls.handle<IBestApplicationDay>(
      this.service.getDayWithMostOpenedApplicaitonAllUsers(),
      this.bestDay
    );
  }

  loadOsStatisticsPeriod() {
    this.ls.handleChart<IOsStatisticResponse>(
      this.service.getOsStatisticUserPeriod(),
      this.pieChart,
      {
        map<IOsStatisticResponse>(response, member) {
          member.data.data = response.pieChart.quantity;
          member.data.labels = response.pieChart.name;
        },
      }
    );
  }

  getTopOpenedAppsPeriod() {
    this.ls.handleChart<ITopApplications>(
      this.service.getTopOpenedAppsPeriod(),
      this.columnChart,
      {
        map<ITopApplications>(response, member) {
          member.data.data = response.chartRecords.value;
          member.data.labels = response.chartRecords.label;
        },
      }
    );
  }
}
