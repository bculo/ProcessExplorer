import { Component, OnInit } from '@angular/core';
import { IProcessItem } from '../../models/process.models';
import { ProcessService } from '../../process.service';

@Component({
  selector: 'app-search-all',
  templateUrl: './search-all.component.html',
  styleUrls: ['./search-all.component.css']
})
export class SearchAllComponent implements OnInit {


  public errorMessage: string | null = null;
  public isLoading: boolean = true;

  public totalRecords: number = 0;
  public records: IProcessItem[] = [];
  public currentPage: number = 1;
  public totalPages: number = 1;
  public totalNumberOfSessions: number = 0;

  public searchCriteria: string = "";

  constructor(private service: ProcessService) { }

  ngOnInit(): void {
    this.getRecords();
  }

  getRecords() {
    this.service.searchAllProcesses(this.searchCriteria, this.currentPage)
      .subscribe(response => {
        this.records = response.records;
        this.totalRecords = response.totalRecords;
        this.totalPages = response.totalPages;
        this.totalNumberOfSessions = response.totalNumberOfSessions;

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

    this.currentPage--;
    this.getRecords();
  }

  next(){
    if(this.currentPage === this.totalPages) return;

    this.currentPage++;
    this.getRecords();
  }

  onSearch(searchCriteria: string){
    if(this.searchCriteria !== searchCriteria) { 
      this.searchCriteria = searchCriteria;
      this.currentPage = 1;
      this.getRecords();
    }
  }
}
