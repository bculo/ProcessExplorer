import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-refresh-page',
  templateUrl: './refresh-page.component.html',
  styleUrls: ['./refresh-page.component.css']
})
export class RefreshPageComponent implements OnInit {

  constructor(private location: Location) { }

  ngOnInit(): void {
  }

  onGoBack() {
    this.location.path();
  }

}
