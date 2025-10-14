import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ImageService } from '../../_services/image.service';
import { environment } from '../../../environments/environment';
import { DiasDetailsComponent } from "../dias-details/dias-details.component";
import {PaginationModule} from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-dia-list',
  standalone: true,
  imports: [DiasDetailsComponent,PaginationModule],
  templateUrl: './dia-list.component.html',
  styleUrl: './dia-list.component.css',
})
export class DiaListComponent implements OnInit {
  baseUrl = environment.apiUrl;
  private route = inject(ActivatedRoute);
  imageService = inject(ImageService);
  
  currentPage = 0;
  smallnumPages = 0;
  pageNumber = 1;
  pageSize = 12;
  categoryId = "";

  ngOnInit(): void {
    this.categoryId = this.route.snapshot.params['id'];// get category id from the route
    this.loadImages(+this.categoryId);
  }
  loadImages(id: number){ this.imageService.getDiasFromCategory(id, this.pageNumber, this.pageSize);}
  getImageFromServer(id: string) {return this.baseUrl + "Images/getImageFile/" + id; }
  pageChanged(event: any){
    if(this.pageNumber != event.page){
      this.pageNumber = event.page;
      this.loadImages(+this.categoryId);
    }
  }
}
