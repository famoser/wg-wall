import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { Product } from '../models/product';
import { FrontendUser } from '../models/frontend-user';

@Injectable({ providedIn: 'root' })
export class ProductService {

  private productUrl = 'api/Product'; // URL to web api

  constructor(private http: HttpClient) {
  }

  get(): Observable<Product[]> {
    return this.http.get<Product[]>(this.productUrl);
  }

  create(product: Product, frontendUser: FrontendUser): Observable<Product> {
    console.log(product.name);
    return this.http.post<Product>(this.productUrl, {
      name: product.name,
      frontendUserId: frontendUser.id
    });
  }

  hide(productName: string): Observable<any> {
    return this.http.get(this.productUrl + "/hideAsRecommendation/" + productName);
  }

  update(product: Product): Observable<any> {
    return this.http.put(this.productUrl + "/" + product.id, product);
  }
}
