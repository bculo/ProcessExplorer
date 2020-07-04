import { Component, OnInit, OnDestroy } from '@angular/core';
import { ApplicationUser } from '../shared/models/application-user.model';
import { Subscription } from 'rxjs';
import { AuthenticationService } from '../authentication/authentication.service';
import { SignalRService } from '../shared/services/signal-r.service';
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit, OnDestroy {
  public authenticatedUser: ApplicationUser;
  
  private userSub: Subscription;
  private pathSub: Subscription;

  private burgerActive = false;

  constructor(private authService: AuthenticationService,
    private router: Router,
    private signalR: SignalRService) { }

  ngOnDestroy(): void {
    this.userSub.unsubscribe();
    this.pathSub.unsubscribe();
  }

  ngOnInit(): void {
    this.userSub = this.authService.user.subscribe((user: ApplicationUser) => {
      if(user) {
        this.authenticatedUser = user;
        this.signalR.startConnection(user);
      }else{
        this.authenticatedUser = null;
        this.signalR.stopConnection();
      }
    });

    this.pathSub = this.router.events
      .pipe(filter(i => i instanceof NavigationEnd))
      .subscribe((instnace: NavigationEnd) => {
        if(instnace.url.indexOf('/authentication') > -1 && this.isAuthenticated())
          this.router.navigate(['/session']);
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
