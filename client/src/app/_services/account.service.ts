import { HttpClient } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { User } from '../_models/User';
import { map } from 'rxjs';
import { environment } from '../../environments/environment';
import { categoryModel } from '../_models/categoryModel';
import { CategoryService } from './category.service';
import { PasswordUpdate } from '../_models/PasswordUpdate';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  currentUser = signal<User | null>(null);
  CatArray = signal<categoryModel[] | null>(null);

  login(model: any){
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map(user => {
        if(user){localStorage.setItem('user', JSON.stringify(user));}
        this.currentUser.set(user);
      })
    );
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUser.set(null);
    this.CatArray.set(null);
  }

  changePwd(pwd: PasswordUpdate){
    return this.http.put<string>(this.baseUrl + 'Account/changePassword/', pwd,{ responseType: 'text' as 'json' })
  }

  
}
