import { Component } from '@angular/core';
import { User } from '../_models/user';
import { AuthenticationService } from '../_services/authentication.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    templateUrl: './login.component.html',
})
export class LoginComponent {

    public username: string;
    public password: string;
    public hasError: boolean;

    constructor(private svc: AuthenticationService, private router: Router) {

    }


    onSubmit(): void {
        this.svc.login(this.username, this.password).subscribe(data => {
            this.hasError = false;
            this.router.navigate(["/"]);
        }, error => {
                this.hasError = true;
        });
    }
}
