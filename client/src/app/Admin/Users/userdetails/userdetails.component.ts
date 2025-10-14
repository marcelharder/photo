import { Component, inject, Input, OnInit } from '@angular/core';
import { User } from '../../../_models/User';
import { UserService } from '../../../_services/user.service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { AccountService } from '../../../_services/account.service';

@Component({
  selector: 'app-userdetails',
  standalone: true,
  imports: [FormsModule, DatePipe],
  templateUrl: './userdetails.component.html',
  styleUrl: './userdetails.component.css'
})
export class UserdetailsComponent implements OnInit {
  @Input() us: User | undefined;
  private usa = inject(UserService);
  private route = inject(Router);
  password = "oldpassword";

  ngOnInit(): void {
    if (this.us?.UserId != null) { this.getFullUserDetails(this.us.UserId); }

  }
  getFullUserDetails(UserId: number) {
    this.usa.getSpecificUser(UserId).subscribe({
      next: (data) => {

        if (this.us != undefined) {
          this.us.AllowedToSee = data.AllowedToSee;
          this.us.Email = data.Email;
          this.us.PhoneNumber = data.PhoneNumber;
          this.us.Created = data.Created;
          this.us.gender = data.gender;
        }

      }
    })
  }



  deleteUser() {
    if (this.us !== undefined) {
      this.usa.deleteUser(this.us.UserId).subscribe(
        {
          next: (data) => {
            this.route.navigateByUrl("/")
          },
        }
      );
    }
  }

 

  editUser() {
    if (this.us !== undefined) {
      this.usa.editUser(this.us).subscribe(
        {
          next: (data) => {
            this.route.navigateByUrl("/")
          },
        }
      );




    }
  }

}
