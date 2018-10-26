import { Component } from '@angular/core';
import {
  faCheck, faSave, faUndo, faTrash, faPencilAlt, faPlus, faChevronUp, faChevronDown
} from '@fortawesome/free-solid-svg-icons';

import { Product } from '../models/product';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: [
    './products.component.css'
  ]
})
export class ProductsComponent {
  //icons
  public faCheck = faCheck;
  public faPlus = faPlus;
  public faPencilAlt = faPencilAlt;
  public faTrash = faTrash;
  public faSave = faSave;
  public faUndo = faUndo;
  public faChevronUp = faChevronUp;
  public faChevronDown = faChevronDown;

  //product lists
  public products: Product[] = [];

  //input
  public newProductName: string = "";
  public isEditActive: boolean = false;
  public onlyActive: boolean = false;

  //to disable buttons when appropiate
  public actionsActive: number;

  //edit entries
  public editSource: Product;
  public editContainer: Product;

  //confirm entry
  public executeSource: Product;

  constructor(private productService: ProductService) { }

  ngOnInit() {
    this.productService.get().subscribe(products => {
      //init view
      this.products = products;
      this.products.sort((a, b) => a.name.localeCompare(b.name));

      if (this.products.length == 0) {
        this.startAdd();
      }
    });
  }

  public startAdd() {
    this.editContainer = new Product();
  }


  public startEdit(source: Product) {
    this.editSource = source;
    this.editContainer = new Product();
    this.editContainer.name = source.name;
  }

  public add(source: Product) {
    //lock
    this.actionsActive++;

    //save to api
    this.actionsActive++;
    source.amount = 1;
    this.productService.create(source).subscribe(newProduct => {
      var added = false;
      for (let i = 0; i < this.products.length; i++) {
        if (this.products[i].name.localeCompare(newProduct.name) > 0) {
          this.products.splice(i, 0, newProduct);
          added = true;
          break;
        }
      }
      //add if not added in loop
      if (!added) {
        this.products.push(newProduct);
      }

      this.actionsActive--;
    });

    //allow to add new directly
    this.editContainer = new Product();
    this.actionsActive--;
  }

  public abort() {
    this.editSource = null;
    this.editContainer = null;
  }

  public save(source: Product, target: Product) {
    //lock
    this.actionsActive++;

    //write props
    target.name = source.name;

    //lock & persist changes
    this.actionsActive++;
    this.productService.update(target).subscribe(() => this.actionsActive--);

    //stop edit
    this.abort();
    this.actionsActive--;
  }

  public remove(subject: Product) {
    //lock
    this.actionsActive++;

    //lock & remove entity
    this.actionsActive++;
    this.productService.remove(subject).subscribe(() => {
      this.products.splice(this.products.indexOf(subject), 1);
      this.actionsActive--;
    });

    //stop edit
    this.abort();
    this.actionsActive--;
  }

  public preparePurchase(product: Product) {
    this.executeSource = product;
  }

  public abortPurchase() {
    this.executeSource = null;
  }

  public confirmPurchase(product: Product) {
    if (product.amount <= 0) {
      return;
    }

    //lock
    this.actionsActive++;

    //register purchase
    this.actionsActive++;
    this.productService.registerPurchase(product).subscribe(() => {
      this.actionsActive--;
    });

    //stop execution
    this.abortPurchase();
    this.actionsActive--;
  }

  public toggleActive(product: Product) {
    console.log("clicked");
    product.amount = product.amount > 0 ? 0 : 1;
    this.productService.update(product).subscribe(() => this.actionsActive--);
  }

  trackByFn(index) {
    return index;
  }
}
