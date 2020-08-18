import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from "@angular/router";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { AuthService } from '../services/auth.service';
import { Login } from "../models/login";

@Component({
  selector: 'app-customer',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  loginForm: FormGroup;
  

  constructor(private fb: FormBuilder,
    private router: Router,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private authService: AuthService) {

  }

  // Page Initializes
  ngOnInit() {
    this.authService.logOff();
    this.loginForm = this.fb.group({
      Username: ["", Validators.required],
      Password: ["", Validators.required]
    });
  }


  // Logs in the User.
  login(login: Login) {
    if (login && login.Username && login.Password) {
      this.authService.login(login).subscribe(result => {
        if (result) {
          this.router.navigate(["/customer"]);
        } else {
          this.router.navigate(["/login"]);
        }
      }, error => console.error(error));
    }
    else {
      this.router.navigate(["/login"]);
    }
  }
}

