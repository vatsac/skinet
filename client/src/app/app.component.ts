import { Component } from '@angular/core';
import { IProduct } from './models/product';
import { IPagination } from './models/pagination';

import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'SkiNet';
  products: IProduct[];

  constructor(private http: HttpClient){}

  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/products?pageSize=50').subscribe(
      (response: IPagination) => {
        this.products = response.data;
      }, error => {
        console.log(error);
      });
  }
}
