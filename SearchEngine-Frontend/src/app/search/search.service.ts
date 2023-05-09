import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
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

  getSearchHistory(): Observable<string[]> {
    return this._http.get<string[]>("http://localhost:9003/api/Histories")
  }

  saveHistory(term: string): Observable<any> {
    const url = 'http://localhost:9003/api/Histories';
    const params = new HttpParams().set('Term', term);
    return this._http.post(url, null, { params });
  }
}
