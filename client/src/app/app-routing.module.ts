import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  {path: '', component: HomeComponent, data: {breadcrumb: 'Home'}},
  {path: 'shop', loadChildren: () => import('./shop/shop.module').then(mod => mod.ShopModule),
  data: {breadcrumb: 'Shop'}},
  {path: '**', redirectTo: '', pathMatch: 'full'} // redirect to homepage if bad url is entered
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
