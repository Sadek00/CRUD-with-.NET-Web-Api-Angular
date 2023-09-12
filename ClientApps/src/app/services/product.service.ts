import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Product } from '../models/product/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }
  getAllProducts(): Observable<any>{
    return this.http.get<any>(`${environment.apiUrl}/${"Product/GetProducts"}`);
  }
  addProducts(addProducts: Product): Observable<any>{
    return this.http.post<any>(`${environment.apiUrl}/${"Product/AddProducts"}`,addProducts);
  }
  editProducts(id:any): Observable<any>{
    return this.http.get<any>(`${environment.apiUrl}/${"Product"}/${id}`);
  }
  updateProducts(id:any,updateProducts: Product): Observable<any>{
    return this.http.put<any>(`${environment.apiUrl}/${"Product"}/${id}`,updateProducts);
  }
  deleteProduct(id:any):Observable<any>{
    return this.http.delete<any>(`${environment.apiUrl}/${"Product"}/${id}`);
  }
}
