import { Component, OnInit, OnDestroy } from '@angular/core';
import { ApplicationUser } from '../shared/models/application-user.model';
import { Subscription } from 'rxjs';
import { AuthenticationService } from '../authentication/authentication.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit, OnDestroy {
  public authenticatedUser: ApplicationUser;
  
  private subscription: Subscription;

  constructor(private authService: AuthenticationService) { }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  ngOnInit(): void {
    this.subscription = this.authService.user.subscribe((user: ApplicationUser) => {
      if(user) {
        this.authenticatedUser = user;
      }else{
        this.authenticatedUser = null;
      }
    });
  }

  onLogout(){
    this.authService.logout();
  }

  isAuthenticated(){
    if(this.authenticatedUser) return true;
    return false;
  }

  

}
