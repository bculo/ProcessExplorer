import { Component, OnInit } from '@angular/core';
import { ApplicationService } from '../../application.service';
import { PaginationComponent } from 'src/app/shared/abstract/pagination-component';
import { IApplicationItem } from '../../models/application.models';

@Component({
  selector: 'app-search-user',
  templateUrl: './search-user.component.html',
  styleUrls: ['./search-user.component.css']
})
export class SearchUserComponent extends PaginationComponent<IApplicationItem> implements OnInit {

  constructor(private service: ApplicationService) { super() }

  ngOnInit(): void {
    this.getRecords();
  }

  getRecords() {
    this.service.searchApplicationsUser(this.searchCriteria, this.currentPage)
      .subscribe(response => {
        this.handleResponse(response);
      },
      (error) => {
        this.handleError();
      });
  }
}
