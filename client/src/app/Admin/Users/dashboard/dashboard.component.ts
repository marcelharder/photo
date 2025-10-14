import { Component, inject, OnInit } from '@angular/core';
import { User } from '../../../_models/User';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../../_services/user.service';
import { environment } from '../../../../environments/environment';
import { NgFor, NgIf } from '@angular/common';
import { UserdetailsComponent } from '../userdetails/userdetails.component';
import { AddUserComponent } from '../add-user/add-user.component';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [UserdetailsComponent, NgFor,AddUserComponent,NgIf],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {
  baseUrl = environment.apiUrl;
  private route = inject(ActivatedRoute);
  private usa = inject(UserService);
  listOfUsers: User[] = [];
  AddPage = 0;

  ngOnInit(): void {
    this.route.data.subscribe({
      next: (data) => {
        this.listOfUsers = data['us'];
   
      },
    });
  }

  showAddPage(){if(this.AddPage == 1){return true;}else{return false;}}

  addUser(){this.AddPage = 1}

}
