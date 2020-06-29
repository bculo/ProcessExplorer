import { Component, OnInit, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent implements OnInit {

  @Output() search = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
  }

  onSearch(searchBy: string) {
    if(!searchBy)
      searchBy = ""
    this.search.emit(searchBy);
  }

}
