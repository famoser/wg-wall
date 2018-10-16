import { Component, ViewChild, Output, EventEmitter, Input } from '@angular/core';
import { Product } from '../models/product';
import { ProductService } from '../services/products.service';
import { FrontendUser } from '../models/frontend-user';
import { faCheck } from '@fortawesome/free-solid-svg-icons';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { faMinus } from '@fortawesome/free-solid-svg-icons';
import { faShoppingCart } from '@fortawesome/free-solid-svg-icons';
import { faTrash } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent {
  //icons
  public faCheck = faCheck;
  public faTrash = faTrash;
  public faPlus = faPlus;
  public faMinus = faMinus;
  public faShoppingCart = faShoppingCart;

  //product lists
  private products: Product[]
  public register: string[];
  public active: Product[];

  //input
  public newProductName: string = ""
  public isLoading: boolean = false;

  //varia
  public showHelp: boolean = true;

  @Input("user") user: FrontendUser

  constructor(private productService: ProductService) { }

  ngOnInit() {
    this.productService.get().subscribe(products => {
      //init view
      this.products = products;
      this.register = Array.from(new Set(this.products.map(p => p.name))).sort();
      this.active = this.products.filter(p => p.boughtById == null && p.amount > 0);
    });
  }

  add() {
    this.select(this.newProductName);
    this.newProductName = "";
  }

  create(name: string) {
    //create product
    const newProduct = new Product();
    newProduct.name = name;

    this.isLoading = true;
    this.productService.create(newProduct, this.user).subscribe(fu => {
      console.log(fu);
      //referesh view
      this.products.push(fu);
      this.register = Array.from(new Set(this.products.map(p => p.name))).sort();
      this.active.push(fu);

      //reset input
      this.isLoading = false;
    });
  }

  update(product: Product) {
    this.isLoading = true;
    this.productService.update(product).subscribe(() => {
      this.isLoading = false;
    });
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

  decrement(product: Product) {
    if (product.amount == 1) {
      this.active.splice(this.active.indexOf(product), 1);
    }
    product.amount = +product.amount - 1;
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
    this.user.karma = +this.user.karma + +product.amount;
  }

  hideHelp() {
    this.showHelp = false;
  }
}
