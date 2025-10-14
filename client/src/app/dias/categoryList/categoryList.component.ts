import { Component, OnInit, inject } from '@angular/core';
import { CategoryService } from '../../_services/category.service';
import { CategoryDetailsComponent } from '../categoryDetails/categoryDetails.component';
import {PaginationModule} from 'ngx-bootstrap/pagination';

@Component({
    selector: 'app-categoryList',
    standalone: true,
    templateUrl: './categoryList.component.html',
    styleUrls: ['./categoryList.component.css'],
    imports: [CategoryDetailsComponent,PaginationModule]
})
export class CategoryListComponent implements OnInit{
  catservice = inject(CategoryService);
  currentPage = 0;
  smallnumPages = 0;
  pageNumber = 1;
  pageSize = 9;

  ngOnInit(){
   // get the array from the account service signal
   //this.SeriesArray = this.accountService.CatArray();
   this.loadCategories();
 // if(!this.catservice.paginatedResult()){
   
 // }

  }

  loadCategories(){
    this.catservice.getAllowedCategories(this.pageNumber, this.pageSize);
  }

  pageChanged(event: any){
    if(this.pageNumber != event.page){
      this.pageNumber = event.page;
      this.loadCategories();
    }
  }
  

}
