import { Component, OnInit } from '@angular/core';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{
  baseUrl = environment.apiUrl; 

  
  ngOnInit(): void {
    
  }

  getFotoFile(id: string) {
    return this.baseUrl + 'Images/getFullImageFile/' + id;
  }

  


}
