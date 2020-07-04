import { Component, OnInit, Input } from '@angular/core';
import { ITabItem } from '../../../abstract/tab-component';

@Component({
  selector: 'app-tab-item',
  templateUrl: './tab-item.component.html',
  styleUrls: ['./tab-item.component.css']
})
export class TabItemComponent implements OnInit {

  @Input() item: ITabItem;
  
  constructor() { }

  ngOnInit(): void {
  }

}
