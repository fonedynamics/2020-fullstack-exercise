import { Injectable, Inject } from '@angular/core';
import { Observable, of } from 'rxjs';
import { tap, delay } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from "@angular/router";
import { Login } from "../models/login";

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  loggedIn = false;

  constructor(private router: Router,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) {
  }

  
  // Creates the Request body for POST.
  private createRequestBody(username: string, password?: string) {
    let body = JSON.stringify({ username, password });
    return body;
  }

  // Logs in the User.
  login(login: Login): Observable<any> {
    let body = this.createRequestBody(login.Username, login.Password);
    const headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }

    return this.http.post(this.baseUrl + "api/User/Login", body, headers)
      .pipe(
        delay(1000),
        tap(val => {
          this.loggedIn = true;
          sessionStorage.setItem("loggedIn", "true");
        })
      );

  }

  isLoggedIn() {
    return !!sessionStorage.getItem("loggedIn");
  }

  logOff() {
    sessionStorage.clear();
    this.router.navigate(["/login/"]);
  }
}
