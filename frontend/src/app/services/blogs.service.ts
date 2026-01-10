import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';
import { Observable } from 'rxjs';
import { Blog } from '../models/blog.model';
import { Guid } from '../models/guid.type';

@Injectable({
  providedIn: 'root'
})
export class BlogsService {

  private readonly base_url= environment.apiUrl+'blogs';

  private readonly POST= this.base_url
  private readonly GET=(id:string)=> `${this.base_url}/${id}`
  private readonly GETALL= this.base_url;
  private readonly DELETE=(id:string)=> `${this.base_url}/${id}`
  private readonly PUT=(id:string)=> `${this.base_url}/${id}`


  constructor(private http: HttpClient) { }

  createBlog(title:string,content:string,categoryId:string){
    return this.http.post(this.POST,{title,content,categoryId});
  }
  editBlog(id:string,title:string,content:string,categoryId:string):Observable<Guid>{
    return this.http.put<Guid>(this.PUT(id),{id,title,content,categoryId});
  }
  delteBlog(id:string){
    this.http.delete(this.DELETE(id))
  }
  getBlog(id:string):Observable<Blog>{
    return this.http.get<Blog>(this.GET(id));
  }

  getAllBlogs():Observable<Blog[]>{
    return this.http.get<Blog[]>(this.GETALL);
  }
}
