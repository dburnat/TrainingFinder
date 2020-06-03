import { AppDataService } from "./../../services/appdata.service";
import { AuthenticationService } from "./../../services/authentication.service";
import { Router } from "@angular/router";
import { Component } from "@angular/core";
import * as Toast from "nativescript-toast";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import { getRootView } from "tns-core-modules/application/application";

@Component({
    selector: "login",
    templateUrl: "login.html",
})
export class LoginComponent {
    public drawer: RadSideDrawer;

    public constructor(
        private router: Router,
        private authenticationService: AuthenticationService,
    ) {
        // redirect to home if already logged in
        if (this.authenticationService.currentUserValue) {
            this.router.navigate(["/"]);
        }
    }

    ngAfterViewInit() {
        setTimeout(() => {
            this.drawer = <RadSideDrawer>getRootView();
            this.drawer.gesturesEnabled = false;
        }, 100);
    }

    public login(userName: string, password: string) {
        if (userName === null || password === null) return;

        return this.authenticationService.login(userName, password).subscribe(
            (result) => {
                this.router.navigate(["home"]);
            },
            (error) => {
                Toast.makeText(error, "long").show();
                console.log(error);
                // this.loading = false;
            }
        );
    }
    public goToRegister() {
        this.router.navigate(["register"]);
    }
}
