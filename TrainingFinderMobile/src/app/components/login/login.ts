import { AppDataService } from "./../../services/appdata.service";
import { AuthenticationService } from "./../../services/authentication.service";
import { Router, ActivatedRoute } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { Component} from "@angular/core";
import { first } from "rxjs/operators";
import * as Toast from "nativescript-toast";

@Component({
    selector: "login",
    templateUrl: "login.html",
})
export class LoginComponent {
    public constructor(
        private router: Router,
        private authenticationService: AuthenticationService,
        private appDataService: AppDataService
    ) {
        // redirect to home if already logged in
        if (this.authenticationService.currentUserValue) {
            this.router.navigate(["/"]);
        }
    }

    public login(userName: string, password: string) {
        if (userName === null || password === null) return;

        return this.authenticationService
            .login(userName, password)
            .subscribe(
                (result) => {
                    this.router.navigate(["home"]);
                },
                (error) => {
                    Toast.makeText(error.message, "long").show();
                    console.log(error);
                    // this.loading = false;
                }
            );
    }
    public goToRegister() {
        this.router.navigate(["register"]);
    }
}
