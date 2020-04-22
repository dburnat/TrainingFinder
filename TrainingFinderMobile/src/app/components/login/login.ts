import { AuthenticationService } from "./../../services/authentication.service";
import { Router, ActivatedRoute } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { first } from "rxjs/operators";

@Component({
    selector: "login",
    templateUrl: "login.html",
})
export class LoginComponent implements OnInit {

    public constructor(
        private httpClient: HttpClient,
        private router: Router,
        private route: ActivatedRoute,
        private authenticationService: AuthenticationService
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
            .pipe(first())
            .subscribe(
                result => {
                    this.router.navigate(["home"]);
                },
                error => {
                    console.log(error);
                    // this.loading = false;
                });
    }
    public goToRegister() {
        this.router.navigate(["register"]);
    }

    ngOnInit(): void {}
}
