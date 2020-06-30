import { Component, OnInit } from '@angular/core';
import { ISessionProcessItem } from '../../models/session.models';
import { ActivatedRoute, Params } from '@angular/router';
import { SessionService } from '../../session.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-details-processes',
  templateUrl: './details-processes.component.html',
  styleUrls: ['./details-processes.component.css']
})
export class DetailsProcessesComponent implements OnInit {

  public errorMessageProcesses: string | null = null;
  public isLoadingProcesses: boolean = true;
  public totalRecordsProcesses: number = 0;
  public recordsProcesses: ISessionProcessItem[] = [];
  public currentPageProcesses: number = 1;
  public totalPagesProcesses: number = 1;

  public id: string;

  constructor(private route: ActivatedRoute,
    private service: SessionService) { }

  ngOnInit(): void {
    this.id = this.service.choosenSession.sessionId;
    this.getProcesses();
  }

  getProcesses() {
    this.service.getSessionProcesses(this.id, this.currentPageProcesses)
      .subscribe(response => {
        this.recordsProcesses = response.records;
        this.totalRecordsProcesses = response.totalRecords;
        this.totalPagesProcesses = response.totalPages;

        this.isLoadingProcesses = false;
        this.errorMessageProcesses = null;
      },
      (error) => {
        this.errorMessageProcesses = "An error occurred";
        this.isLoadingProcesses = false;
      });
  }

  backProcesses(){
    if(this.currentPageProcesses === 1) return;

    this.currentPageProcesses--;
    this.getProcesses();
  }

  nextProcesses(){
    if(this.currentPageProcesses === this.totalPagesProcesses) return;

    this.currentPageProcesses++;
    this.getProcesses();
  }
}
