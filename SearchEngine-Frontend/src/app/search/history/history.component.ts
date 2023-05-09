import {Component, OnInit} from '@angular/core';
import {SearchService} from "../search.service";
import {sample} from "rxjs";

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.scss']
})
export class HistoryComponent implements OnInit {

  searchHistory: string[] | undefined

  constructor(private _searchService: SearchService) {
  }

  ngOnInit(): void {
    this._searchService.getSearchHistory().subscribe(history => {
      this.searchHistory = history;
    });
  }

  protected readonly sample = sample;
}
