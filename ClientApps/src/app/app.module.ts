import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { RegisterComponent } from './Component/register/register.component';
import { LoginComponent } from './Component/register/login/login.component';
import { AddProductComponent } from './Component/Product/add-product/add-product.component';
import { ProductListComponent } from './Component/Product/product-list/product-list.component';
import { EditProductComponent } from './Component/Product/edit-product/edit-product.component';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    AddProductComponent,
    ProductListComponent,
    EditProductComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
