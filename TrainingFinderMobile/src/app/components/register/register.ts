import { environment } from './../../../environments/environment';
import { Component } from "@angular/core";
import { HttpClient, HttpHeaders, HttpRequest } from "@angular/common/http";
import { Router } from "@angular/router";
import * as Toast from "nativescript-toast";
import "rxjs/Rx";

@Component({
    selector: "register",
    templateUrl: "register.html",
})
export class RegisterComponent {
    public constructor(private http: HttpClient, private router: Router) {}

    public register(
        userName: string,
        firstName: string,
        lastName: string,
        password: string
    ) {
        this.http
            .post(
                `${environment.apiUrl}/users/register`,
                {
                    firstName: firstName,
                    lastName: lastName,
                    username: userName,
                    password: password,
                }
            )
            .subscribe(
                (result) => {
                    Toast.makeText("Account created", "long").show();
                    this.router.navigate(["login"]);
                },
                (error) => {
                    Toast.makeText(error.message, "long").show();
                    console.log(error);
                }
            );
    }
}
