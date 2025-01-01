import { CurrencyPipe, DatePipe, Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductsService } from '../../Services/Products/Products.service';
import { Product } from '../../Models/Product/product';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { MessagesModule } from 'primeng/messages';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-detalis-product',
  imports: [ToastModule ,MessagesModule , CardModule  ,CurrencyPipe ,DatePipe ,ButtonModule],
  templateUrl: './detalis-product.component.html',
  styleUrl: './detalis-product.component.css' ,
  providers: [MessageService]
})
export class DetalisProductComponent implements OnInit {





  productId: string = '';
  proudectDetials:Product


  constructor(private loc:Location , private route: ActivatedRoute , private productService:ProductsService ,  private messageService: MessageService , private rou:Router) {
    this.proudectDetials={
      createdDate:'',
      description:'' ,
      price:0,
      name:'',
      id:0 ,
      urlImageProduct:''
 }
}
ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.productId = params.get('id')!;
      this.loadProductData();
    });
  }

  loadProductData() {
    this.productService.getProductById(Number(this.productId)).subscribe({
      next: (product) => {
       this.proudectDetials=product
       console.log(this.proudectDetials)
      },
      error: (err) => {
        this.messageService.add({ severity: 'error', summary: 'Load Failed', detail: 'Failed to load product data. Please try again later.' });
        console.error("Error loading product:", err);
        setTimeout(() => {
          this.rou.navigate(['/product']);
        }, 3000);
      }
    });
  }


  back(){

    this.loc.back()

 }

}
