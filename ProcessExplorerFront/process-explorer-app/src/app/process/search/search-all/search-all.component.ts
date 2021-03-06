import { Component, OnInit } from '@angular/core';
import { IProcessItem } from '../../models/process.models';
import { ProcessService } from '../../process.service';
import { PaginationComponent } from 'src/app/shared/abstract/pagination-component';

@Component({
  selector: 'app-search-all',
  templateUrl: './search-all.component.html',
  styleUrls: ['./search-all.component.css']
})
export class SearchAllComponent extends PaginationComponent<IProcessItem> implements  OnInit {

  public totalNumberOfSessions: number = 0;

  constructor(private service: ProcessService) { super() }

  ngOnInit(): void {
    this.getRecords();
  }

  getRecords() {
    this.service.searchAllProcesses(this.searchCriteria, this.currentPage)
      .subscribe(response => {
        this.handleResponse(response);
        this.totalNumberOfSessions = response.totalNumberOfSessions;
      },
      (error) => {
        this.handleError();
      });
  }
}
