import { Injectable } from "@angular/core";
import { Product } from "../models/product";

@Injectable({
  providedIn: "root",
})
export class ProductService {
  public getProducts(): Product[] {
    let products = [
      new Product({
        id: "1",
        name: "black and white nike sneakers",
        category: "fashion",
        price: "85",
        image: "./assets/images/solesavy-QGcRQiUV-Vc-unsplash.jpg",
      }),
      new Product({
        id: "2",
        name: "black converse sneakers",
        category: "fashion",
        price: "60",
        image: "./assets/images/camila-damasio-mWYhrOiAgmA-unsplash.jpg",
      }),
      new Product({
        id: "3",
        name: "yellow tracksuit",
        category: "fashion",
        price: "100",
        image: "./assets/images/dom-hill-nimElTcTNyY-unsplash.jpg",
      }),
      new Product({
        id: "4",
        name: "brown leather coat",
        category: "fashion",
        price: "250",
        image: "./assets/images/dami-adebayo-k6aQzmIbR1s-unsplash.jpg",
      }),
      new Product({
        id: "5",
        name: "colorful keyboard",
        category: "gaming",
        price: "300",
        image: "./assets/images/mateo-vrbnjak-nCU4yq5xDEQ-unsplash.jpg",
      }),
      new Product({
        id: "6",
        name: "PS4",
        category: "gaming",
        price: "400",
        image: "./assets/images/nikita-kachanovsky-mwytIca3qNA-unsplash.jpg",
      }),
      new Product({
        id: "7",
        name: "Xbox",
        category: "gaming",
        price: "400",
        image: "./assets/images/louis-philippe-poitras-WMMh6BtmTMo-unsplash.jpg",
      }),
      new Product({
        id: "8",
        name: "black headphones",
        category: "gaming",
        price: "100",
        image: "./assets/images/frank-septillion-Qrspubmx6kE-unsplash.jpg",
      }),
    ];
    return products;
  }
}
