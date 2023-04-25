import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: 'user', loadChildren: () =>
  import('./user/user.module').then(value => value.UserModule)},

  {path: 'search', loadChildren: () =>
  import('./search/search.module').then(value => value.SearchModule)}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
