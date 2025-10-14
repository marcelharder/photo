import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule, BsDropdownModule, RouterLink, RouterLinkActive],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent implements OnInit {
  accountService = inject(AccountService);
  router = inject(Router);
  model: any = {};
  currentUserName? = "";

  ngOnInit(): void {

  }

  /**
   *
   */
  constructor(private toastr: ToastrService) {
    
    
  }

  login() {
    this.accountService.login(this.model).subscribe({
      next: response => {
        this.currentUserName = this.accountService.currentUser()?.UserName;
      },
      error: error => { console.log(error);this.toastr.warning('Wrong password') },
      complete: () => { }
    })




    console.log(this.model);
  }

  logout() {
    this.router.navigate(['/']);
    this.accountService.logout();
  }

}
