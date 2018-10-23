import { Component, Input } from '@angular/core';
import {
    faCheck, faEyeSlash, faMinus, faPencilAlt, faPlus
} from '@fortawesome/free-solid-svg-icons';

import { FrontendUser } from '../models/frontend-user';
import { Product } from '../models/product';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html'
})
export class ProductsComponent {
  //icons
  public faCheck = faCheck;
  public faPlus = faPlus;
  public faMinus = faMinus;
  public faPencilAlt = faPencilAlt;
  public faEyeSlash = faEyeSlash;

  //product lists
  private products: Product[]
  public register: string[] = [];
  public active: Product[] = [];

  //input
  public newProductName: string = ""
  public isEditActive: boolean = false;

  @Input() user: FrontendUser

  constructor(private productService: ProductService) { }

  ngOnInit() {
    this.productService.get().subscribe(products => {
      //init view
      this.products = products;
      this.active = this.products;
      this.populateRegister();
      if (this.products.length == 0) {
        this.isEditActive = true;
      }
    });
  }

  create(name: string) {
    //create product
    const newProduct = new Product();
    newProduct.name = name;

    this.productService.create(newProduct, this.user).subscribe(fu => {
      //referesh view
      this.products.push(fu);
      this.active.push(fu);
      this.populateRegister();
    });
  }

  populateRegister() {
    this.register = Array.from(new Set(this.products.map(p => p.name))).sort();
  }

  update(product: Product) {
    this.productService.update(product);
  }

  select(name: string) {
    const existing = this.active.filter(p => p.name == name);
    if (existing.length > 0) {
      existing[0].amount = +existing[0].amount + 1;
      this.update(existing[0]);
    } else {
      this.create(name);
    }
  }

  add() {
    //add if not existing
    const existing = this.active.filter(p => p.name == this.newProductName);
    if (existing.length == 0) {
      this.create(this.newProductName);
    }

    //reset view
    this.newProductName = "";
  }

  abortEdit() {
    this.isEditActive = false;
  }

  decrement(product: Product) {
    if (product.amount == 1) {
      this.active.splice(this.active.indexOf(product), 1);
    }
    product.amount = product.amount - 1;
    this.update(product);
  }

  increment(product: Product) {
    product.amount = product.amount + 1;
    this.update(product);
  }

  confirmBuy(product: Product) {
    this.active.splice(this.active.indexOf(product), 1);
    this.update(product);
    this.user.karma = +this.user.karma + +product.amount;
  }

  hide(name: string) {
    this.productService.hideAll(name);
    this.products.filter(p => p.name == name);
    this.populateRegister();
  }

  enableEdit() {
    this.isEditActive = true;
  }
}
