import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { Observable } from 'rxjs';
import { Product } from '../../Models/Product/product';
import { UpdateProduct } from '../../Models/Product/update-product';
import { CreateProduct } from '../../Models/Product/create-product';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  Controller ='products/'
  constructor(private readonly  Clinent:HttpClient) { }

  getAllProudects(): Observable<Product[]> {
    return this.Clinent.get<any>(`${environment.baseUrl}${this.Controller}${environment.FunNameForProduct.GetAll}`);
  }

  deleteProduct(id: number): Observable<Product[]> {
    return this.Clinent.delete<any>(`${environment.baseUrl}${this.Controller}${environment.FunNameForProduct.Delete}/${id}`);
  }

  getProductById(id:number): Observable<Product> {
    return this.Clinent.get<any>(`${environment.baseUrl}${this.Controller}${environment.FunNameForProduct.GetById}/${id}`);
  }

  getAllProudectsByName(name:string): Observable<Product[]> {
    return this.Clinent.get<any>(`${environment.baseUrl}${this.Controller}${environment.FunNameForProduct.Search}/name`);
  }

  updateProduct(id:number ,product: any): Observable<Product[]> {
    return this.Clinent.put<any>(`${environment.baseUrl}${this.Controller}${environment.FunNameForProduct.Update}/${id}`, product);
  }

  createProduct(prodct:any): Observable<Product[]> {
    return this.Clinent.post<any>(`${environment.baseUrl}${this.Controller}${environment.FunNameForProduct.Create}`,prodct);
  }




}
