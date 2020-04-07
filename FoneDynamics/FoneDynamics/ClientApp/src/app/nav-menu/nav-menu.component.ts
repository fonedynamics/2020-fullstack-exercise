import { Component } from '@angular/core';
import { AuthenticationService } from '../_services/authentication.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
    constructor(private authenticationService: AuthenticationService) {

    }
    logout(): void {
        this.authenticationService.logout();
    }
  
}
