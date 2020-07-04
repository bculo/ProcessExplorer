import { Component, OnInit, Input } from '@angular/core';
import { ITabClient } from 'src/app/shared/abstract/tab-component';

@Component({
  selector: 'app-page-tab-navigation',
  templateUrl: './page-tab-navigation.component.html',
  styleUrls: ['./page-tab-navigation.component.css']
})
export class PageTabNavigationComponent implements OnInit {

  @Input() client: ITabClient;

  constructor() { }

  ngOnInit(): void {
  }

}
