import { Component, OnInit } from '@angular/core';
import { ProcessService } from '../../process.service';
import { IProcessItem } from '../../models/process.models';
import { PaginationComponent } from 'src/app/shared/abstract/pagination-component';

@Component({
  selector: 'app-search-user',
  templateUrl: './search-user.component.html',
  styleUrls: ['./search-user.component.css']
})
export class SearchUserComponent extends PaginationComponent<IProcessItem> implements OnInit {

  public totalNumberOfSessions: number = 0;

  constructor(private service: ProcessService) { super() }

  ngOnInit(): void {
    this.getRecords();
  }

  getRecords() {
    this.service.searchUserProcesses(this.searchCriteria, this.currentPage)
      .subscribe(response => {
        this.handleResponse(response);
        this.totalNumberOfSessions = response.totalNumberOfSessions;
      },
      (error) => {
        this.handleError();
      });
  }
}
