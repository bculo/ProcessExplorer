import { Component, OnInit } from '@angular/core';
import { ISessionProcessItem } from '../../models/session.models';
import { ActivatedRoute } from '@angular/router';
import { SessionService } from '../../session.service';
import { PaginationComponent } from 'src/app/shared/abstract/pagination-component';

@Component({
  selector: 'app-details-processes',
  templateUrl: './details-processes.component.html',
  styleUrls: ['./details-processes.component.css']
})
export class DetailsProcessesComponent extends PaginationComponent<ISessionProcessItem> implements OnInit {

  public id: string;

  constructor(private route: ActivatedRoute,
    private service: SessionService) {  super() }

  ngOnInit(): void {
    this.id = this.service.choosenSession.sessionId;
    this.getRecords();
  }

  getRecords() {
    this.service.getSessionProcesses(this.id, this.currentPage)
      .subscribe(response => {
        this.handleResponse(response);
      },
      (error) => {
        this.handleError();
      });
  }
}
