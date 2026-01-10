import { Routes } from '@angular/router';
import { DefaultComponent } from './layouts/default/default.component';
import { HomeComponent } from './components/home/home.component';
import { AdminComponent } from './layouts/admin/admin.component';
import { BlogComponent } from './components/admin/blog/blog.component';
import { CategoryListComponent } from './components/admin/category-list/category-list.component';

export const routes: Routes = [
  { path:'', component:DefaultComponent ,

  children: [
    {path:'', component:HomeComponent},
  ]
},

{ path:'admin',component:AdminComponent,
  //canActivate:[AuthGuard],
  children:[
    {path:'category',component:CategoryListComponent},
    //canActivate:[AuthGuard]},
    {path:'blog',component:BlogComponent},
    //canActivate:[AuthGuard]},
  ]
}

];
