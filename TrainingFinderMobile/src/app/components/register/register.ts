import { map } from "rxjs/operators";
import { AuthenticationService } from "./../../services/authentication.service";
import { Component } from "@angular/core";
import { HttpClient, HttpHeaders, HttpRequest } from "@angular/common/http";
import { Router } from "@angular/router";
import "rxjs/Rx";

@Component({
    selector: "register",
    templateUrl: "register.html",
})
export class RegisterComponent {
    public constructor(private http: HttpClient, private router: Router) {}

    public register(userName: string, firstName:string, lastName:string, password: string) {
        let headers = new HttpHeaders();
        headers.append("Content-Type", "application/json");
        headers.append("Access-Control-Allow-Origin", "*");
        // headers.append('Accept', '*/*');
        this.http
            .post(
                "http://192.168.0.104:4000/users/register",
                {
                    firstName: firstName,
                    lastName: lastName,
                    username: userName,
                    password: password,
                },
                { headers: headers }
            )
            .subscribe(
                (result) => {
                    this.router.navigate(["home"], {
                        queryParams: { jwt: result },
                    });
                },
                (error) => {
                    // Toast.makeText(error.json().message).show();
                    console.log(error);
                }
            );
    }
}
