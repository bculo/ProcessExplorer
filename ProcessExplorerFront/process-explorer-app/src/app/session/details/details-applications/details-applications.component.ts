import { Component, OnInit } from '@angular/core';
import { PaginationComponent } from 'src/app/shared/abstract/pagination-component';
import { ISessionApplicationItem } from '../../models/session.models';
import { ActivatedRoute } from '@angular/router';
import { SessionService } from '../../session.service';
import { ILoadingMember, IChartModel } from 'src/app/shared/models/interfaces.models';

@Component({
  selector: 'app-details-applications',
  templateUrl: './details-applications.component.html',
  styleUrls: ['./details-applications.component.css']
})
export class DetailsApplicationsComponent extends PaginationComponent<ISessionApplicationItem> implements OnInit {

  //column chart stats
  public columnChart: ILoadingMember<IChartModel> = {
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
    },
    isLoading: true,
    errorMessage: null //string
  }

  public id: string;

  constructor(private route: ActivatedRoute,
    private service: SessionService) {  super() }

  ngOnInit(): void {
    this.id = this.service.choosenSession.sessionId;
    this.getRecords();
    this.getTopAppsForSession();
  }

  getRecords() {
    this.service.getSessionApplications(this.id, this.currentPage)
      .subscribe(response => {
        console.log(response);
        this.handleResponse(response);
      },
      (error) => {
        this.handleError();
      });
  }

  getTopAppsForSession(){
    this.service.getTopSessionApplications(this.id)
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

}
