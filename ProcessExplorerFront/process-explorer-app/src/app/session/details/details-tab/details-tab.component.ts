import { Component, OnInit, Input } from '@angular/core';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-details-tab',
  templateUrl: './details-tab.component.html',
  styleUrls: ['./details-tab.component.css']
})
export class DetailsTabComponent implements OnInit {

  private subscription: Subscription;
  @Input() id: string;

  public processPath = "";
  public appsPath = "";

  constructor() { }

  ngOnInit(): void {
    this.processPath = `/session/details/${this.id}/processes`;
    this.appsPath = `/session/details/${this.id}`;
  }
}
