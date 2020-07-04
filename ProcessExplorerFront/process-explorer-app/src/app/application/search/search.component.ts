import { Component, OnInit } from '@angular/core';
import { ITabClient, ITabItem } from 'src/app/shared/abstract/tab-component';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements ITabClient, OnInit {

  constructor() { }

  tabItems: ITabItem[] = [
    {
      route: '/application/search',
      exactRoute: true,
      icon: 'fa fa-users',
      title: 'All applications'
    },
    {
      route: '/application/search/user',
      exactRoute: false,
      icon: 'fa fa-user',
      title: 'My applications'
    }
  ];

  ngOnInit(): void {
  }

}
