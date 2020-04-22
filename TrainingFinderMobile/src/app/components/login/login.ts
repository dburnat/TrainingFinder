import { AuthenticationService } from "./../../services/authentication.service";
import { Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";

@Component({
    selector: "login",
    templateUrl: "login.html",
})
export class LoginComponent implements OnInit {
    public constructor(
        private httpClient: HttpClient,
        private router: Router,
        private authenticationService: AuthenticationService
    ) {}

    public login(userName: string, password: string) {
        this.authenticationService.login(userName, password);
        console.log("login function");
    }
    public goToRegister() {
        this.router.navigate(["register"]);
        console.log("click register");
    }

    ngOnInit(): void {}
}
