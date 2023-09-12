import { Component } from '@angular/core';
import { Route, Router } from '@angular/router';
import { Product } from 'src/app/models/product/product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent {

  product: Product={
    id: 0,
  categoryId: 0,
  categoryName: '',
  unitName: '',
  name: '',
  code: '',
  parentCode: '',
  productBarcode: '',
  description: '',
  brandName: '',
  sizeName: '',
  colorName: '',
  modelName: '',
  variantName: '',
  oldPrice: 0,
  price: 0,
  costPrice: 0,
  stock: 0,
  totalPurchase: 0,
  lastPurchaseDate: '',
  lastPurchaseSupplier: '',
  totalSales: 0,
  lastSalesDate: '',
  lastSalesCustomer: '',
  type: '',
  status: '',
  commissionAmount: 0,
  commissionPer: 0,
  pctn: 0,
  }

  constructor(private productService: ProductService, private router: Router){}
  addProducts() {
    this.productService.addProducts(this.product).subscribe({
      next:(product)=>{
        this.router.navigate(['product/product-list']);
      }
    })
  }
}
