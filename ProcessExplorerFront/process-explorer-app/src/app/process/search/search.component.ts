import { Component, OnInit } from '@angular/core';
import { ITabItem, ITabClient } from 'src/app/shared/abstract/tab-component';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements ITabClient, OnInit {

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
