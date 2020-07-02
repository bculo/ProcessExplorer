import { Component, OnInit } from '@angular/core';
import { ICommunication } from './models/admin.models';
import { AdminService } from './admin.service';
import { ILoadingMember } from '../shared/models/interfaces.models';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  public types: ILoadingMember<ICommunication[]> = {
    data: [], //where data IBestProcessesDay
    isLoading: true,
    errorMessage: null //string
  }

  constructor(private service: AdminService) { }

  ngOnInit(): void {
    this.getAllCommunicationTypes();
  }

  getAllCommunicationTypes() {
    this.service.getAll().subscribe(
      (response) => {
        this.types.data = response.types;

        this.types.isLoading = false;
        this.types.errorMessage = null;
      },
      (error) => {
        this.types.isLoading = false;
        this.types.errorMessage = "Ann error occured";
      }
    )
  }

  chnageCommunicationType(type: number){
    this.service.changeType(type).subscribe(
      () => {
        this.getAllCommunicationTypes();
      }
    );
  }

}
