import { Routes } from '@angular/router';
import { DefaultComponent } from './layouts/default/default.component';
import { HomeComponent } from './components/home/home.component';
import { AdminComponent } from './layouts/admin/admin.component';
import { BlogComponent } from './components/admin/blog/blog.component';
import { CategoryListComponent } from './components/admin/category-list/category-list.component';
import { DashboardComponent } from './components/admin/dashboard/dashboard.component';
import { LoginComponent } from './components/authentication/login/login.component';
import { RegisterComponent } from './components/authentication/register/register.component';
import { AuthGuard } from './core/services/auth-guard.service';
import { ProfileComponent } from './components/users/profile/profile.component';
import { UserComponent } from './layouts/user/user.component';
import { EditAddressComponent } from './components/users/edit-address/edit-address.component';

export const routes: Routes = [
  { path:'', component:DefaultComponent ,

  children: [
    {path:'', component:HomeComponent},
  ]
  },
  { path:'admin',component:AdminComponent, canActivate:[AuthGuard],
    children:[
      {path:'', component:DashboardComponent},
      {path:'dashboard',component:DashboardComponent,canActivate:[AuthGuard]},
      {path:'category',component:CategoryListComponent,canActivate:[AuthGuard]},
      {path:'blog',component:BlogComponent,canActivate:[AuthGuard]},
      ]
  },
  {path:'login', component : LoginComponent},
  {path:'register', component : RegisterComponent},
  {path:'user',component : UserComponent, canActivate:[AuthGuard],
    children:[
      {path:'profile', component:ProfileComponent, canActivate:[AuthGuard]},
      {path:'edit-address',component: EditAddressComponent, canActivate:[AuthGuard]}
    ]
  }

];
