import { CommonModule, Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { InputNumberModule } from 'primeng/inputnumber';
import { FileUploadModule } from 'primeng/fileupload';
import { ButtonModule } from 'primeng/button';
import { ProductsService } from '../../Services/Products/Products.service';
import { CreateProduct } from '../../Models/Product/create-product';
import { MessageService } from 'primeng/api';
import { MessagesModule } from 'primeng/messages';
import { ToastModule } from 'primeng/toast';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-product',
  imports: [CommonModule , InputNumberModule, FileUploadModule , ButtonModule, ReactiveFormsModule ,  MessagesModule,
    ToastModule],
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.css'] ,
  providers: [MessageService]
})
export class CreateProductComponent implements OnInit {

  productForm!: FormGroup;
  isImageValid: boolean = true;

  constructor(private fb: FormBuilder  ,    private productService: ProductsService, private messageService: MessageService , private loc:Location , private rou:Router ){}

  ngOnInit() {
    this.productForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(500)]],
      price: [null, [Validators.required, Validators.min(1)]],
      image: new FormControl(null, Validators.required)
    });
  }

  onFileSelect(event: any) {
    const file = event.files[0];
    const fileType = file.type.split('/')[0];
    if (fileType === 'image') {
      this.isImageValid = true;
      this.productForm.controls['image'].setValue(file);
    } else {
      this.isImageValid = false;
      this.productForm.controls['image'].setValue(null);
    }
  }

  onSubmit() {
    if (this.productForm.valid) {
      const formData = new FormData();
    formData.append('name', this.productForm.value.name);
    formData.append('description', this.productForm.value.description);
    formData.append('price', this.productForm.value.price);
    formData.append('imageProduct', this.productForm.value.image);


      this.productService.createProduct(formData).subscribe({
        next: () => {
          this.messageService.add({severity: 'success', summary: 'Success', detail: 'Product created successfully!'});

            setTimeout(() => {
              this.rou.navigate(['/product/all']);
            }, 2000);
        },
        error: (err) => {
          this.messageService.add({severity: 'error', summary: 'Error', detail: 'Please try again later!'});


        }
    })
  }


}


back(){

   this.loc.back()

}

}
