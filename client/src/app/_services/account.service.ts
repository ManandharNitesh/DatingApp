import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators'
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root',
})

//providedIn:root
//so no need to mention in providers in appmodule
export class AccountService {
  baseUrl = 'https://localhost:5001/api/';
  // | null Manually added
  private currentUserSource = new ReplaySubject<User | null>(1);//size of buffer how many user//current user object
 //convetion $ at end for observable
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) {}


  login(model:any){
    // return this.http.post(this.baseUrl+'account/login',model).pipe(
    return this.http.post<User>(this.baseUrl+'account/login',model).pipe(
      map((response : User )=>{
        const user = response;
        if(user){
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }

  setCurrentUser(user: User){
    this.currentUserSource.next(user);
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  register(model:any)
  {
    return this.http.post<User>(this.baseUrl+'account/register',model).pipe(
      map((user: User) => {
        if(user){
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUserSource.next(user);
        }
        // return user;
      })
    );
  }
}
