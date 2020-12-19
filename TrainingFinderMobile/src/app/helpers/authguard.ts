import { AuthenticationService } from './../services/authentication.service';
import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';



@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) : boolean {
        const currentUser = this.authenticationService.currentUserValue;
        if (currentUser.token) {
            // logged in so return true
            return true;
        }

        // not logged in so redirect to login page with the return url
        console.log("user not logged");
        this.router.navigate(["login"], { queryParams: { returnUrl: state.url } });
        return false;
    }
}
