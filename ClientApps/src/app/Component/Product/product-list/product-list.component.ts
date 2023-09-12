import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from 'src/app/models/product/product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit  {
  products: Product[] = [];
  constructor(private productService: ProductService, private router: Router){}
  
  ngOnInit(): void{
    this.productService.getAllProducts().subscribe({
      next:(product:any)=>{
         this.products=product;
        console.log(product);
      },
      error:(response)=>{
        console.log(response);
      }
    })
  }
  deleteProducts(id:any){
    this.productService.deleteProduct(id).subscribe({
      next:(a:any)=>{
        this.router.navigate(['product/product-list']);
      }
    });
  }
}
