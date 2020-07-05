import { Component, OnInit, ViewChild, ElementRef, Renderer2 } from '@angular/core';
import { AuthenticationService } from './authentication/authentication.service';
import { Router } from '@angular/router';
import { SignalRService } from './shared/services/signal-r.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'process-explorer-app';

  @ViewChild('notification') private notification: ElementRef;
  public isVisible = false;

  private sub: Subscription;
  private timeout: any;

  constructor(private authService: AuthenticationService,
    private signalR: SignalRService) { }

  ngOnInit(): void {
    this.authService.autoLogin();

    this.sub = this.signalR.sync.subscribe(() => {
      if(!this.isVisible){
        this.isVisible = true;
        this.timeout = setTimeout(() => this.remove(), 5000);
      }
      if(this.isVisible){
        clearTimeout(this.timeout);
        this.timeout = setTimeout(() => this.remove(), 5000);
      }
    });
  }

  remove(): void {
    this.isVisible = false;
  }
}
