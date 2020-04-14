import { map } from 'rxjs/operators';
import { AuthenticationService } from './../../services/authentication.service';
import { Component } from "@angular/core";
import { HttpClient, HttpHeaders, HttpRequest } from "@angular/common/http";
import { Router } from "@angular/router";
import "rxjs/Rx";


@Component({
    selector: "login",
    templateUrl: "register.html",
})
export class RegisterComponent {

    public constructor(private http: HttpClient, private router: Router) { }

    public login(username: string, password: string) {
        let headers = new HttpHeaders();
        headers.append('Content-Type', 'application/json');
        headers.append('Access-Control-Allow-Origin', '*');
        this.http.post("https://localhost:4000/users/register",
        JSON.stringify({ firstName: "d", lastName: "b", username: username, password: password }), {headers: headers})
            .subscribe(result => {
                this.router.navigate(["home"], { queryParams: { jwt: result } });
            }, error => {
                // Toast.makeText(error.json().message).show();
                console.log(error);
            })
            ;
    }

}
