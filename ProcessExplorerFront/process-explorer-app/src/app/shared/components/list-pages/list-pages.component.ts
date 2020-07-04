import { Component, OnInit, Input } from '@angular/core';
import { IPageChanger } from '../../abstract/pagination-component';

@Component({
  selector: 'app-list-pages',
  templateUrl: './list-pages.component.html',
  styleUrls: ['./list-pages.component.css']
})
export class ListPagesComponent implements OnInit {

  @Input() client: IPageChanger;

  constructor() { }

  ngOnInit(): void {
    if(this.client === null)
      throw new Error("Cant be null");
  }
}
