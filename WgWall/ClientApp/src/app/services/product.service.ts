import { Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Product } from '../models/product';
import { FrontendUserService } from './frontend-user.service';
import { ReloadService } from './reload.service';
import { switchMap, tap } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class ProductService {

  private productPurchase = 'api/ProductPurchase'; // URL to web api
  private productUrl = 'api/Product'; // URL to web api

  constructor(private http: HttpClient, private reload: ReloadService, private frontendUserService: FrontendUserService) {
  }

  public get(): Observable<Product[]> {
    return this.reload.reloadObservable.pipe(
      switchMap(() => this.http.get<Product[]>(this.productUrl))
    )
  }

  public create(product: Product): Observable<Product> {
    return this.http.post<Product>(this.productUrl, this.toJsonPayload(product));
  }

  public update(product: Product): Observable<any> {
    return this.http.put(this.productUrl + "/" + product.id, this.toJsonPayload(product));
  }

  public remove(product: Product): Observable<any> {
    return this.http.delete(this.productUrl + "/" + product.id);
  }

  public registerPurchase(product: Product): Observable<any> {
    return this.frontendUserService.getActiveUser().pipe(
      switchMap(frontendUser => {
        return this.http.post(this.productPurchase, {
          frontendUserId: frontendUser.id,
          ProductId: product.id
        }).pipe(
          tap(() => {
            //adapt connected entites
            frontendUser.karma += product.amount;
            product.amount = 0;
          })
        )
      })
    );
  }

  private toJsonPayload(product: Product) {
    return {
      name: product.name,
      amount: product.amount
    };
  }
}
