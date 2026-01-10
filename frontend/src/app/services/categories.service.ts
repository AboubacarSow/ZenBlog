import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.dev';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Category } from '../models/category.model';
import { Guid } from '../models/guid.type';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {

  private readonly base_url= environment.apiUrl+'categories';

  private readonly POST= this.base_url
  private readonly GET=(id:string)=> `${this.base_url}/${id}`;
  private readonly GETALL= this.base_url;
  private readonly DELETE=(id:string)=> `${this.base_url}/${id}`;
  private readonly PUT=(id:string)=> `${this.base_url}/${id}`;

  constructor(private http: HttpClient) { }

  createCategory(name:string):Observable<Category>{
    return this.http.post<Category>(this.POST,{name});
  }

  editCategory(id:Guid,name:string){
    this.http.put(this.PUT(id),{id,name})
  }

  delteCategory(id:Guid){
    this.http.delete(this.DELETE(id))
  }
  getCategory(id:Guid):Observable<Category>{
    return this.http.get<Category>(this.GET(id));
  }
  getAllCategories():Observable<Category[]>{
    return this.http.get<Category[]>(this.GETALL);
  }
  
}
