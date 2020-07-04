import { Component, OnInit } from '@angular/core';
import { ApplicationService } from '../../application.service';
import { IApplicationItem } from '../../models/application.models';
import { PaginationComponent } from 'src/app/shared/abstract/pagination-component';

@Component({
  selector: 'app-search-all',
  templateUrl: './search-all.component.html',
  styleUrls: ['./search-all.component.css']
})
export class SearchAllComponent extends PaginationComponent<IApplicationItem> implements OnInit {

  constructor(private service: ApplicationService) { super() }

  ngOnInit(): void {
    this.getRecords();
  }

  getRecords() {
    this.service.searchApplicationsPeriod(this.searchCriteria, this.currentPage)
      .subscribe(response => {
        this.handleResponse(response);
      },
      (error) => {
        this.handleError();
      });
  }
}
