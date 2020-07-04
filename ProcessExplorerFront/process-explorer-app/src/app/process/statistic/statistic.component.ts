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
      route: '/process/search',
      exactRoute: true,
      icon: 'fa fa-users',
      title: 'All processes'
    },
    {
      route: '/process/search/user',
      exactRoute: false,
      icon: 'fa fa-user',
      title: 'My processes'
    }
  ];

  ngOnInit(): void {
  }

}
