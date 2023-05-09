import { Component } from '@angular/core';
import {Observable} from "rxjs";
import {Search} from "./search";
import {Document} from "./document";
import {SearchService} from "./search.service";

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent {

  searchResult: Observable<Search> | undefined;
  results: Document[] | undefined

  hostName: string | undefined

  searchTerm: string | undefined;

  constructor(private _searchService: SearchService) {
  }

  getSearchResult(searchTerm: string) {
    this.results = [];
    this._searchService.getSearchResult(searchTerm).subscribe(value => {
      console.log(value.documents);
      this.results = value.documents;
      this.hostName = value.hostName;
    });
    this._searchService.saveHistory(searchTerm).subscribe(value => {
    })
  };


}
