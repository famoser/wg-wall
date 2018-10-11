import { Component, ViewChild, Output, EventEmitter, Input } from '@angular/core';
import { Product } from '../models/product';
import { ProductService } from '../services/products.service';
import { FrontendUser } from '../models/frontend-user';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html'
})
export class ProductsComponent {
  private products: Product[]
  public newProductName: String = ""
  public isLoading: Boolean = false;

  public register: String[];
  public active: Product[];

  @Input("user") user: FrontendUser

  constructor(private productService: ProductService) { }

  ngOnInit() {
    this.productService.get().subscribe(products => {
      this.products = products;
      this.active = this.products.filter(p => p.boughtById == null);
      this.register = Array.from(new Set(this.products.map(p => p.name))).sort();
    });
  }

  create(product: Product) {
    this.isLoading = true;
    this.productService.create(product, this.user).subscribe(fu => {
      this.products.push(fu);
      this.active.push(fu);
      this.newProductName = "";
      this.isLoading = false;
    });
  }

  update(product: Product) {
    this.isLoading = true;
    this.productService.update(product).subscribe(fu => {
      this.isLoading = false;
    });
  }

  select(name: String) {
    const existing = this.active.filter(p => p.name == name);
    if (existing.length > 0) {
      existing[0].amount = +existing[0].amount + 1;
      this.update(existing[0]);
    } else {
      const newProduct = new Product();
      newProduct.name = name;
      this.create(newProduct);
    }
  }

  decrement(product: Product) {
    product.amount = +product.amount - 1;
    if (product.amount == 1) {
      this.active.splice(this.active.indexOf(product), 1);
    }
    this.update(product);
  }

  increment(product: Product) {
    product.amount = +product.amount + 1;
    this.update(product);
  }

  confirmBuy(product: Product) {
    product.boughtById = this.user.id;
    this.active.splice(this.active.indexOf(product), 1);
    this.update(product);
  }
}
