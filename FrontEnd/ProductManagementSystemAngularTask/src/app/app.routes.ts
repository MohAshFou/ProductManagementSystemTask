import { Routes } from '@angular/router';
import { ProductComponent } from './Component/product/product.component';
import { AllProducstComponent } from './Component/all-producst/all-producst.component';
import { CreateProductComponent } from './Component/create-product/create-product.component';
import { UpdateProductComponent } from './Component/update-product/update-product.component';
import { DetalisProductComponent } from './Component/detalis-product/detalis-product.component';
import { NotFoundComponent } from './Component/NotFound/not-found/not-found.component';

export const routes: Routes = [

  {
    path: "",
    redirectTo: "product/all",
    pathMatch: "full"
  },
  {
    path: "product",
    component: ProductComponent , children:[
        {path:"all",
          component:AllProducstComponent

        } ,
        {path:"create",
          component:CreateProductComponent

        } ,
        {path:"update/:id",
          component:UpdateProductComponent

        }
        ,
        {path:"details/:id",
          component:DetalisProductComponent

        }



    ]
  },

  {
    path: "**",
    component: NotFoundComponent
  }


];
