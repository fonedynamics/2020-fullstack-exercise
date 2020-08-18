import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';
import { User } from '../_models/user';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';


@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<any>;
    public currentUser: Observable<User>;
    private userLoggedIn = new Subject<boolean>();

    constructor(private http: HttpClient, private router: Router) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(sessionStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
        this.userLoggedIn.next(false);
    }

    public get currentUserValue(): any {
        return this.currentUserSubject.value;
    }

    login(username: string, password: string) {

        return this.http.post<any>(`${environment.apiUrl}/auth/token`, { username: username, password: password }).pipe(
            map(
                data => {
                    sessionStorage.setItem('currentUser', JSON.stringify(data));
                    this.currentUserSubject.next(data);
                }));

    }
    logout() {
        // remove user from session storage to log user out
        sessionStorage.removeItem('currentUser');
        this.userLoggedIn.next(false);
        this.currentUserSubject.next(null);
        this.router.navigate(["/login"]);

    }
}
