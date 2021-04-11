import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { PageEvent } from '@angular/material';
import { Product } from '../models/product';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ProductListComponent implements OnInit {
  products: Product[] = []
  lowValue: number = 0;
  highValue: number = 4;
  pageSize: number = 4;

  constructor(private productService: ProductService) { }

  ngOnInit() {
    return this.productService.getProducts().subscribe(
      products => {
          this.products = products;
      }
  );
  }

  pageChanged(event: PageEvent): PageEvent {
    this.lowValue = event.pageIndex * event.pageSize;
    this.highValue = this.lowValue + event.pageSize;
    return event;
  }

}
