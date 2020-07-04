import { Component, OnInit } from '@angular/core';
import { ITabItem, ITabClient } from 'src/app/shared/abstract/tab-component';

@Component({
  selector: 'app-statistic',
  templateUrl: './statistic.component.html',
  styleUrls: ['./statistic.component.css']
})
export class StatisticComponent implements ITabClient, OnInit {

  constructor() { }

  tabItems: ITabItem[] = [
    {
      route: '/session',
      exactRoute: true,
      icon: 'fa fa-users',
      title: 'All sessions'
    },
    {
      route: '/session/user',
      exactRoute: false,
      icon: 'fa fa-user',
      title: 'My sessions'
    }
  ];

  ngOnInit(): void {
  }

}
