import {Component, OnInit} from '@angular/core';
import {SearchService} from "../search.service";

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.scss']
})
export class HistoryComponent implements OnInit {

  history: string[] | undefined

  constructor(private _searchService: SearchService) {
  }

  ngOnInit(): void {
  }

}
