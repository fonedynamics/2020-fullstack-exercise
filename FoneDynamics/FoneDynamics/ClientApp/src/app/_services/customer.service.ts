import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';
import { User } from '../_models/user';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';
import { Customer } from '../_models/customer';


@Injectable({ providedIn: 'root' })
export class CustomerService {

   
  
    constructor(private http: HttpClient) {
       

    }


    public fetch(sortCol: string, sortAsc:boolean, numOfEmployee: number, searchKey: string, skip: number, take: number) {

        return this.http.get<Customer[]>(`${environment.apiUrl}/customers?sortCol=${sortCol}&sortAsc=${sortAsc}&numOfEmployee=${numOfEmployee}&searchKey=${searchKey}&skip=${skip}&take=${take}`).pipe(map(response => {
            return response;
        }));

    }
   
}
