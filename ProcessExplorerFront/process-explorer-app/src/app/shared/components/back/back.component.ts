import { Component, OnInit, Input } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-back',
  templateUrl: './back.component.html',
  styleUrls: ['./back.component.css']
})
export class BackComponent implements OnInit {

  @Input() uri: string;

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  onGoBack() {
    this.router.navigate([this.uri]);
  }
}
