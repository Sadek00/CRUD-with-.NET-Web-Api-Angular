import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from 'src/app/models/product/product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})
export class EditProductComponent {
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

  constructor(private route: ActivatedRoute, private productService:ProductService, private router:Router){}
  ngOnInit():void{
  this.route.paramMap.subscribe({
    next:(params)=>{
      const id = params.get('id');
      if(id){
        this.productService.editProducts(id).subscribe({
          next: (response)=>{
              this.product=response;
          }
        });
      }
    }
  })
}
updateProducts(){
  this.productService.updateProducts(this.product.id,this.product).subscribe({
    next:(response)=>{
      this.router.navigate(['product/product-list']);
    }
  });
}
}
