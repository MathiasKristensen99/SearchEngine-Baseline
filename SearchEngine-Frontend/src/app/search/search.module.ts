import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SearchRoutingModule } from './search-routing.module';
import {SearchComponent} from "./search.component";
import { HistoryComponent } from './history/history.component';


@NgModule({
  declarations: [
    SearchComponent,
    HistoryComponent
  ],
  imports: [
    CommonModule,
    SearchRoutingModule
  ]
})
export class SearchModule { }
