import { Component, OnInit } from '@angular/core';
import { ILoadingMember, IChartExtendedModel, IOsStatisticResponse } from 'src/app/shared/models/interfaces.models';
import { IBestApplicationDay, ITopApplications, IOpenedAppsPerSessionResponse } from '../../models/application.models';
import { ApplicationService } from '../../application.service';
import { LoadingMemberService } from 'src/app/shared/services/loading-member.service';
import { TabItemComponent } from 'src/app/shared/components/tab-navigation/tab-item/tab-item.component';

@Component({
  selector: 'app-statistic-user',
  templateUrl: './statistic-user.component.html',
  styleUrls: ['./statistic-user.component.css']
})
export class StatisticUserComponent implements OnInit {

  //best day stats
  public bestDay: ILoadingMember<IBestApplicationDay>;

  //column chart stats -> TOP 10 applications
  public columnChart: ILoadingMember<IChartExtendedModel>;

  //OS statistics
  public pieChart: ILoadingMember<IChartExtendedModel>;
  
  //Number of oppened apps per session
  public lineChart: ILoadingMember<IChartExtendedModel>;

  constructor(private service: ApplicationService,
    private ls: LoadingMemberService) { }

  ngOnInit(): void {
    this.prepareCharts();
    this.getBestDayAllUsers();
    this.loadOsStatisticsPeriod();
    this.getTopOpenedAppsPeriod();
    this.getOpenedAppsPerSession();
  }

  prepareCharts(): void {
    this.bestDay = this.ls.createMember<IBestApplicationDay>();
    this.pieChart = this.ls.createChart('doughnut', 'Number of opened applications (Last month period)', [{ backgroundColor:[ '#E0FF4F', '#00272B'] }], true);
    this.lineChart = this.ls.createChart('line', 'Number of oppened apps per session (Last 20 sessions)', [{ backgroundColor: ['#95C623'] }]);
    this.columnChart = this.ls.createColumnChart('Top 10 applications (Last month period)', '#FF6663');
  }

  getBestDayAllUsers() {
    this.ls.handle<IBestApplicationDay>
    (
      this.service.getDayWithMostOpenedApplicaitonSingleUser(),
      this.bestDay
    );
  }

  loadOsStatisticsPeriod() {
    this.ls.handleChart<IOsStatisticResponse>
    (
      this.service.getOsStatisticUserPeriod(),
      this.pieChart,
      {
        map<IOsStatisticResponse>(response, member){
          member.data.data = response.pieChart.quantity;
          member.data.labels = response.pieChart.name;
        }
      }
    );
  }

  getTopOpenedAppsPeriod(){
    this.ls.handleChart<ITopApplications>
    (
      this.service.getTopOpenedAppsUserPeriod(),
      this.columnChart,
      {
        map<ITopApplications>(response, member){
          member.data.data = response.chartRecords.value;
          member.data.labels = response.chartRecords.label;
        }
      }
    );
  }

  getOpenedAppsPerSession(){
    this.ls.handleChart<IOpenedAppsPerSessionResponse>
    (
      this.service.getNumberOfOpenedAppsPerSession(),
      this.lineChart,
      {
        map<ITopApplications>(response, member){
          member.data.data = response.chart.number;
          member.data.labels = response.chart.date;
        }
      }
    );
  }
}
