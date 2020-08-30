import { CheckoutModule } from './checkout/checkout.module';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  {path: '', component: HomeComponent, data: {breadcrumb: 'Home'}},
  {path: 'shop', loadChildren: () => import('./shop/shop.module').then(mod => mod.ShopModule),
  data: {breadcrumb: 'Shop'}},
  {path: 'basket', loadChildren: () => import('./basket/basket.module').then(mod => mod.BasketModule),
  data: {breadcrumb: 'Basket'}},
  {path: 'checkout', loadChildren: () => import('./checkout/checkout.module').then(mod => mod.CheckoutModule),
  data: {breadcrumb: 'CheckOut'}},
  {path: '**', redirectTo: '', pathMatch: 'full'} // redirect to homepage if bad url is entered
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
