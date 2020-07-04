import { Component, OnInit } from '@angular/core';
import { SessionService } from '../session.service';
import { ISessionItem } from '../models/session.models';
import { PaginationComponent } from 'src/app/shared/abstract/pagination-component';

@Component({
  selector: 'app-session-search',
  templateUrl: './session-search.component.html',
  styleUrls: ['./session-search.component.css']
})
export class SessionSearchComponent extends PaginationComponent<ISessionItem> implements OnInit {

  constructor(private service: SessionService) {  super() }

  ngOnInit(): void {
    this.getRecords();
  }

  getRecords(){
    this.service.getSessionsForUsers(this.currentPage)
      .subscribe(response => {
        this.handleResponse(response);
      },
      (error) => {
        this.handleError();
      });
  }
}
