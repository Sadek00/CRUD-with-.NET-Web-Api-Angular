import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './Component/register/register.component';
import { LoginComponent } from './Component/register/login/login.component';
import { AddProductComponent } from './Component/Product/add-product/add-product.component';
import { ProductListComponent } from './Component/Product/product-list/product-list.component';
import { EditProductComponent } from './Component/Product/edit-product/edit-product.component';

const routes: Routes = [
  {
    path:'',
    component:LoginComponent
  },
  {
    path:'register',
    component:RegisterComponent
  },
  {
    path:'login',
    component:LoginComponent
  },
  {
    path:'product/add-product',
    component:AddProductComponent
  },
  {
    path:'product/product-list',
    component:ProductListComponent
  },
  {
    path:'product/edit-product/:id',
    component:EditProductComponent
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
