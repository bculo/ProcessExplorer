import { Component, OnInit } from '@angular/core';
import { SessionService } from '../../session.service';

@Component({
  selector: 'app-operating-system-statistic',
  templateUrl: './operating-system-statistic.component.html',
  styleUrls: ['./operating-system-statistic.component.css']
})
export class OperatingSystemStatisticComponent implements OnInit {

  sessionData: number[] = [];
  sessionLabels: string[] = [];
  type="doughnut";

  chartColors: any[] = [
  { 
    backgroundColor:["#FF7360", "#6FC8CE", "#FAFFF2", "#FFFCC4", "#B9E8E0"] 
  }];

  constructor(private service: SessionService) { }

  ngOnInit(): void {
    this.service.getSessionStatistic().subscribe(
      (response) => {
        const pieData = response.pieChartRecords;
        pieData.forEach(i => {
          this.sessionData.push(i.quantity);
          this.sessionLabels.push((i.name) ? i.name : "Linux");
        });
      },
      (error) => {
        console.log(error);
      });
  }

}
