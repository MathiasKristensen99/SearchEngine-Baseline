import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Search} from "./search";

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor(private _http: HttpClient) { }

  getSearchResult(input: string): Observable<Search> {
    return this._http.get<Search>("http://localhost:9000/api/LoadBalancer?terms=" + input)
  }
}
