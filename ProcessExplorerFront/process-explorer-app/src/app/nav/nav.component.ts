import { Component, OnInit, OnDestroy } from '@angular/core';
import { ApplicationUser } from '../shared/models/application-user.model';
import { Subscription } from 'rxjs';
import { AuthenticationService } from '../authentication/authentication.service';
import { SignalRService } from '../shared/services/signal-r.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit, OnDestroy {
  public authenticatedUser: ApplicationUser;
  
  private subscription: Subscription;

  private burgerActive = false;

  constructor(private authService: AuthenticationService,
    private router: Router,
    private signalR: SignalRService) { }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  ngOnInit(): void {
    this.subscription = this.authService.user.subscribe((user: ApplicationUser) => {
      console.log(user);

      if(user) {
        this.authenticatedUser = user;
        this.signalR.startConnection(user);
      }else{
        this.authenticatedUser = null;
        this.signalR.stopConnection();
      }
    });
  }

  onLogout(){
    this.authService.logout();
    this.router.navigate(['/authentication']);
  }

  isAuthenticated(){
    if(this.authenticatedUser) return true;
    return false;
  }

  show(){
    this.burgerActive = !this.burgerActive;
  }

  isBurgerActive(){
    return this.burgerActive;
  }

}
