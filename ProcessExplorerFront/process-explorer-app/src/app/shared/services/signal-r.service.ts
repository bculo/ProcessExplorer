import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel, HubConnectionState } from "@microsoft/signalr";
import { environment } from 'src/environments/environment';
import { ApplicationUser } from '../models/application-user.model';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  private hubConnection: HubConnection;
  private hubUrl: string = environment.hub;

  constructor() { }

  public startConnection(user: ApplicationUser) {
    if(user === null) return;

    //create hubconnection
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl, {
        accessTokenFactory: () => user.token
      })
      .configureLogging(LogLevel.Information)
      .build();

    //start connection
    this.hubConnection.start().then(() => {
      console.log(this.hubConnection.state);
    }).catch((err) => {
      console.log(err);
    });
  }

  public stopConnection() {
    if(!this.hubConnection) return;

    if(this.hubConnection.state === HubConnectionState.Connected)
      this.hubConnection.stop();
  }
}
