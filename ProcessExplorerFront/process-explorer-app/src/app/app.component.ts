import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from './authentication/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'process-explorer-app';

  constructor(private authService: AuthenticationService) { }

  ngOnInit(): void {
    this.authService.autoLogin();
  }
}
