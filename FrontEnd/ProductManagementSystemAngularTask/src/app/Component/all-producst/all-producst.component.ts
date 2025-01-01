import { Component, OnInit } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { ProductsService } from '../../Services/Products/Products.service';
import { Product } from '../../Models/Product/product';
import { MessageService  , ConfirmationService } from 'primeng/api';

import { Router } from '@angular/router';
import { CommonModule, CurrencyPipe, Location } from '@angular/common';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-all-producst',
  imports: [TableModule , ButtonModule , CurrencyPipe , ConfirmDialogModule ,InputTextModule  ,FormsModule , CommonModule],
  templateUrl: './all-producst.component.html',
  styleUrl: './all-producst.component.css' ,
  providers: [MessageService , ConfirmationService]
})
export class AllProducstComponent implements OnInit {


Products:Product[]=[]
  first = 0;
  totalRecords = 100;
  rows = 10;
  customers = [];

  searchText: string = '';

  filteredProduct:Product[] =[]
  constructor( private _productserv:ProductsService , private messageService: MessageService , private rou:Router , private confirmDialogService: ConfirmationService , private loc:Location ){


  }
  ngOnInit(): void {

    this.loadProducts();

  }



   loadProducts() {
            this._productserv.getAllProudects().subscribe({

              next: (data) => {
              this.Products=data
              this.filteredProduct = [...this.Products];
              console.log(this.Products)
              },
              error: (err) => {
                console.log(err)
              }
            })
   }



  //  pageChange(pageEvent: any) {
  //   if (pageEvent) {
  //     this.first = pageEvent.first;
  //     this.rows = pageEvent.rows;
  //     this.loadProducts();
  //   }
  // }

  // isFirstPage() {
  //   return this.first === 0;
  // }

  // isLastPage() {
  //   return this.first === (this.totalRecords - this.rows);
  // }

  // next() {
  //   if (!this.isLastPage()) {
  //     this.first += this.rows;
  //     this.loadProducts();
  //   }
  // }

  // prev() {
  //   if (!this.isFirstPage()) {
  //     this.first -= this.rows;
  //     this.loadProducts();
  //   }
  // }

  // reset() {
  //   this.first = 0;
  //   this.loadProducts();
  // }


  editProduct(product: Product) {
    this.rou.navigate([`product/update/${product.id}`])
  }

  confirmDelete(product: Product) {
    this.confirmDialogService.confirm({
      message: 'Are you sure you want to delete this product?',
      header: 'Confirmation',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.deleteProduct(product);
      },
      reject: () => {
        this.messageService.add({ severity: 'info', summary: 'Cancelled', detail: 'Product deletion cancelled' });
      }
    });
  }
  deleteProduct(product: any) {
    this._productserv.deleteProduct(product.id).subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'Product Deleted', detail: 'The product was successfully deleted.' });
        this.loadProducts();
      },
      error: (err) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to delete the product. Please try again.' });
        console.error('Error deleting product:', err);
      }
    });
  }
  showMoreOptions(product: any) {
    this.rou.navigate([`product/details/${product.id}`])
  }


  filterProducts() {
    console.log(this.searchText)
    if (this.searchText) {
      this.filteredProduct = this.Products.filter(product =>
        product.name.toLowerCase().includes(this.searchText.toLowerCase())
      );


    } else {
      this.filteredProduct = [...this.Products];
    }
  }
  CreateProudct(){

    this.rou.navigate([`product/create`])
  }
  back(){

    this.loc.back()

 }
}
