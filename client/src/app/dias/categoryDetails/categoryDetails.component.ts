import { Component, Input,inject } from '@angular/core';
import { categoryModel } from '../../_models/categoryModel';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-categoryDetails',
  standalone: true,
  imports: [],
  templateUrl: './categoryDetails.component.html',
  styleUrls: ['./categoryDetails.component.css']
})
export class CategoryDetailsComponent {
  private router = inject(Router);
  baseUrl = environment.apiUrl;

  @Input() selectedCategory: categoryModel = {
    Id: 0,
    Description: '',
    MainPhoto: '',
    YearTaken: 0,
  };

  goDetails(id: number) { this.router.navigate(['/diaList/'+id]);  }
  getImageFromServer(id: string) { return this.baseUrl + 'Images/getImageFile/' + id;  }
}
