import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductsService } from '../../Services/Products/Products.service';
import { FileUploadModule } from 'primeng/fileupload';
import { ButtonModule } from 'primeng/button';
import { CommonModule, Location } from '@angular/common';
import { InputNumberModule } from 'primeng/inputnumber';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { MessagesModule } from 'primeng/messages';
import { window } from 'rxjs';

@Component({
  selector: 'app-update-product',
  imports: [CommonModule, InputNumberModule, FileUploadModule, ButtonModule, ReactiveFormsModule, MessagesModule, ToastModule],
  templateUrl: './update-product.component.html',
  styleUrls: ['./update-product.component.css'],
  providers: [MessageService]
})
export class UpdateProductComponent implements OnInit {

  productForm!: FormGroup;
  currentImage: string = '';
  productId: string = '';
  imageChanged: boolean = false;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private productService: ProductsService,
    private router: Router,
    private messageService: MessageService,
    private loc: Location
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.productId = params.get('id')!;
      this.loadProductData();
    });

    this.productForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(500)]],
      price: [null, [Validators.required, Validators.min(1)]],
      image: new FormControl(null)
    });

    // Subscribe to form value changes to enable the Update button if there is any change
    this.productForm.valueChanges.subscribe(() => {
      this.imageChanged = this.productForm.get('image')?.value !== null;
    });
  }

  loadProductData() {
    this.productService.getProductById(Number(this.productId)).subscribe({
      next: (product) => {
        this.productForm.patchValue({
          name: product.name,
          description: product.description,
          price: product.price,
          image: product.urlImageProduct
        });
        this.currentImage = product.urlImageProduct;
      },
      error: (err) => {
        this.messageService.add({ severity: 'error', summary: 'Load Failed', detail: 'Failed to load product data. Please try again later.' });
        console.error("Error loading product:", err);
        setTimeout(() => {
          this.router.navigate(['/product']);
        }, 3000);
      }
    });
  }

  onFileSelect(event: any) {
    const file = event.files[0];
    const fileType = file.type.split('/')[0];
    if (fileType === 'image') {
      this.imageChanged = true;
      this.productForm.controls['image'].setValue(file);
    } else {
      this.imageChanged = false;
      this.productForm.controls['image'].setValue(null);
    }
  }

  clearImage() {
    this.imageChanged = true;
    this.productForm.controls['image'].setValue(null);
    this.currentImage = '';
  }

  onSubmit() {
    if (this.productForm.valid) {
      const productData = this.productForm.value;
      productData.id = this.productId;




      const formData = new FormData();
      formData.append('id', this.productForm.value.id);
      formData.append('name', this.productForm.value.description);
      formData.append('description', this.productForm.value.description);
      formData.append('price', this.productForm.value.price);
      if (this.imageChanged== true) {
        formData.append('newimageproduct', this.productForm.value.image);
      }

      this.productService.updateProduct(productData.id, formData).subscribe({
        next: (response) => {
          this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Product updated successfully!' });
          setTimeout(() => {
            this.router.navigate(['/product/all']);
          }, 2000);
        },
        error: (err) => {
          this.messageService.add({ severity: 'error', summary: 'Update Failed', detail: 'Failed to update product. Please try again later.' });
          console.error("Error updating product:", err);
        }
      });
    } else {
      this.messageService.add({ severity: 'warn', summary: 'Form Invalid', detail: 'Please fill in the form correctly before submitting.' });
    }
  }

  back() {
    this.loc.back();
  }
}
