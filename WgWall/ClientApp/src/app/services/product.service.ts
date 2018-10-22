import { Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { FrontendUser } from '../models/frontend-user';
import { Product } from '../models/product';

@Injectable({ providedIn: 'root' })
export class ProductService {

  private productUrl = 'api/Product'; // URL to web api

  constructor(private http: HttpClient) {
  }

  get(): Observable<Product[]> {
    return this.http.get<Product[]>(this.productUrl);
  }

  create(product: Product, frontendUser: FrontendUser): Observable<Product> {
    return this.http.post<Product>(this.productUrl, {
      name: product.name,
      frontendUserId: frontendUser.id
    });
  }

  hideAll(productName: string) {
    this.http.get(this.productUrl + "/hideAll/" + productName).subscribe(() => {});
  }

  update(product: Product) {
    this.http.put(this.productUrl + "/" + product.id, product).subscribe(() => {});
  }
}
