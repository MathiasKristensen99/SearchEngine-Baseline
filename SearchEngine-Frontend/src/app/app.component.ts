import { Component } from '@angular/core';
import {SearchService} from "./search.service";
import {Observable} from "rxjs";
import {Search} from "./search";
import {Document} from "./document";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'SearchEngine-Frontend';

  searchResult: Observable<Search> | undefined;
  results: Document[] | undefined;

  searchTerm: string | undefined;

  constructor(private _searchService: SearchService) {
  }

  getSearchResult(searchTerm: string) {
    this.results = [];
    this._searchService.getSearchResult(searchTerm).subscribe(value => {
      console.log(value.documents);
      this.results = value.documents;
    });
  };

}
