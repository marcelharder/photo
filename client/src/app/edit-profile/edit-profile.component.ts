import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { UserService } from '../_services/user.service';
import { PasswordUpdate } from '../_models/PasswordUpdate';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-edit-profile',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css'
})
export class EditProfileComponent implements OnInit{
  private acc = inject(AccountService);
  private usa = inject(UserService);
  private router = inject(Router);
  userId? = 0;
  pwd: PasswordUpdate = {
    Email: '',
    Password: '',
    ConfirmPassword: '',
    CurrentPassword: ''
  }

  constructor(private toastr: ToastrService){}

  ngOnInit(): void {
    // get email from the loggedin user
    this.userId = this.acc.currentUser()?.UserId;
    if(this.userId != undefined){this.getFullUserDetails(this.userId);}
    
   }

   getFullUserDetails(UserId: number) {
    this.usa.getSpecificUser(UserId).subscribe({
      next: (data) => { this.pwd.Email = data.Email;}
    })
  }
  

  public Cancel(){ 
     //show toast
    this.toastr.warning('Cancelled ...');
    this.router.navigateByUrl("/")}
  public Update(){

    this.acc.changePwd(this.pwd).subscribe({
      next: () => {
        //show toast
        this.toastr.success('Password changed ...');
        this.router.navigateByUrl("/");
         
        
      }
    })
  }

}
