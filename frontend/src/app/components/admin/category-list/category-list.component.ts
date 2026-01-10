import { NgFor } from '@angular/common';
import { Component } from '@angular/core';
import { Category } from '../../../models/category.model';

@Component({
  selector: 'app-category-list',
  imports: [NgFor],
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.css'
})
export class CategoryListComponent {
categories:Category[]=[];

delete(arg0: any) {
throw new Error('Method not implemented.');
}

}
