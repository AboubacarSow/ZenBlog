import { NgFor, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Category } from '../../../models/category.model';
import { CategoriesService } from '../../../services/categories.service';
import {Modal} from 'bootstrap'
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import *  as alertify from 'alertifyjs'
import { Guid } from '../../../models/guid.type';


@Component({
  selector: 'app-category-list',
  imports: [NgFor, NgIf, ReactiveFormsModule],
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.css'
})
export class CategoryListComponent implements OnInit{

  categories:Category[]=[];
  message : string = ''
  selectedCategory : Category | null = null;
  submitted=false;
  isSaving = false;
  isLoading = false;
  editForm! : FormGroup
  addForm!: FormGroup
  constructor(private categoryService: CategoriesService, private formBuilder: FormBuilder){
    this.editForm = this.formBuilder.group({
      id:[''],
      name:['']
    })

    this.addForm = this.formBuilder.group({
      name:['', Validators.required]
    })
  }

  ngOnInit(): void {
    this.categoryService.getAllCategories().subscribe({
      next:response=>{
        if(!response){
          this.message = " There is no category available yet!"
          return;
        }
        this.categories = response.categories;
        console.log('Get all categories')
      }
    })
  }

  openEditModal(category:any){
     this.editForm.patchValue({
      id:category.id,
      name: category.name
    });
    const modalElement = document.getElementById('editModal')
    if(modalElement){
      const modal = new Modal(modalElement);
      modal.show();
    }
  }


  openAddModal(){
    const modalElement = document.getElementById('addModal')
    if(modalElement){
      const modal  = new Modal(modalElement)
      modal.show();
    }
  }

  create(){
    this.submitted = true;
    if(this.addForm.invalid) return ;

    this.isLoading = true;

    const category_name = this.addForm.value.name;

    this.categoryService.createCategory(category_name).subscribe({
      next:response=>{
        if (response) {
         this.categories.push(response)
          this.submitted = false;
          this.isLoading = false;
          const modelEl = document.getElementById('addModal')
          Modal.getInstance(modelEl!)?.hide();
          alertify.success('Add operation successed!')
          return;
        }
        else{
          alertify.error('Something went wrong')
        }
      },
      error: err=>{
        console.error(err);
        this.isLoading = false
      }
    })
  }

  save() {
    this.submitted = true;
    if(this.editForm.invalid) return;

    this.isSaving = true;
    const category = {
      id: this.editForm.value.id,
      name: this.editForm.value.name
    }
    this.categoryService.editCategory(category.id,category.name).subscribe({
      next:()=>{
        const index = this.categories.findIndex(c=>c.id === category.id)
        if(index != -1){
          this.categories[index].name = category.name;
        }
        this.submitted = false;
        this.isSaving = false;
        const modelEl = document.getElementById('editModal')
        Modal.getInstance(modelEl!)?.hide();
        alertify.success('Edit successed!')
      },
      error: err=>{
        console.error(err);
        this.isSaving = false;
      }
    })


  }

  delete(id: Guid, name:string) {
    const dialog = alertify.confirm(
      `Deleting ${name}`,
      'This action cannot be undone. Continue?',
      () => {
        this.categoryService.delteCategory(id).subscribe(() => {
          alertify.success('Category deleted');
          this.categories = this.categories.filter(c => c.id !== id);
        });
      },
      () => {
        alertify.message('Deletion cancelled');
      }
    );

  dialog
    .set('labels', { ok: 'Delete', cancel: 'Cancel' })
    .set('closable', false)
    .set('modal', true)
    .set('onshow', () => {
      (dialog as any).elements.root.classList.add('alertify-danger');
    });

  }

}
