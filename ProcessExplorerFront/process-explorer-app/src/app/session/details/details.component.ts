import { Component, OnInit, OnDestroy } from '@angular/core';
import { SessionService } from '../session.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Params } from '@angular/router';
import { ISingleSession, ISessionProcessItem } from '../models/session.models';
import { ILoadingMember } from 'src/app/shared/models/interfaces.models';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit, OnDestroy  {

  private subscription: Subscription;
  public id: string;

  public session: ILoadingMember<ISingleSession> = {
    data: null,
    errorMessage: null,
    isLoading: true
  }

  constructor(private route: ActivatedRoute,
    private service: SessionService) { }

  ngOnInit(): void {
    this.subscription = this.route.params.subscribe(
      (params: Params) => {
        this.session.isLoading = true;
        this.session.errorMessage = null;
        this.session.data = null;

        this.id = params['id'];

        this.service.choosenSession.sessionId = this.id;
        this.fetchSession();
      }
    )
  }

  fetchSession(){
    this.service.getSelectedSession(this.id)
      .subscribe((response) => {
        this.session.data = response;
        this.session.isLoading = false;
        this.session.errorMessage = null;
      },
      (error) => {
        this.session.isLoading = false;
        this.session.errorMessage = "Ann error occured";
      });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
    this.service.choosenSession.sessionId = null;
  }
}
