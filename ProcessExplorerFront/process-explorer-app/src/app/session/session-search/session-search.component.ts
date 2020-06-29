import { Component, OnInit } from '@angular/core';
import { SessionService } from '../session.service';
import { ISessionItem } from '../models/session.models';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-session-search',
  templateUrl: './session-search.component.html',
  styleUrls: ['./session-search.component.css']
})
export class SessionSearchComponent implements OnInit {

  public errorMessage: string | null = null;
  public isLoading: boolean = true;

  public totalRecords: number = 0;
  public records: ISessionItem[] = [];
  public currentPage: number = 1;
  public totalPages: number = 1;
  
  constructor(private service: SessionService) { }

  ngOnInit(): void {
    this.getRecords();
  }

  getRecords(){
    this.service.getSessionsForUsers(this.currentPage)
      .subscribe(response => {
        this.records = response.records;
        this.totalRecords = response.totalRecords;
        this.totalPages = response.totalPages;

        this.isLoading = false;
        this.errorMessage = null;
      },
      (error) => {
        this.errorMessage = "An error occurred";
        this.isLoading = false;
      });
  }

  back(){
    if(this.currentPage === 1) return;
    this.getRecords();
  }

  next(){
    if(this.currentPage === this.totalPages) return;
    this.getRecords();
  }
}
