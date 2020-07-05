import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel, HubConnectionState } from "@microsoft/signalr";
import { environment } from 'src/environments/environment';
import { ApplicationUser } from '../models/application-user.model';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  private hubConnection: HubConnection;
  private hubUrl: string = environment.hub;

  public userNum = new BehaviorSubject<number>(1);
  public userLogedOut = new Subject();
  public sync = new Subject();

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
      this.subscribe();
      this.userNotification();
      this.syncNotification();
    }).catch((err) => {
      console.log(err);
    });
  }

  private subscribe(): void {
    this.hubConnection.invoke("Subscribe");
  }

  private userNotification(): void {
    this.hubConnection.on("CreateNotificationForLogin", (data) => {
      this.userNum.next(data);
    });

    this.hubConnection.on("CreateNotificationForLogout", () => {
      this.userLogedOut.next();
    });
  }

  private syncNotification(): void {
    this.hubConnection.on("CreateSyncNotification", () => {
      this.sync.next();
    });
  }

  public stopConnection() {
    if(!this.hubConnection) return;
    if(this.hubConnection.state === HubConnectionState.Connected)
      this.hubConnection.stop();
  }
}
