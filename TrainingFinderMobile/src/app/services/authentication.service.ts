import { environment } from "./../../environments/environment";
import { User } from "../models/user.model";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BehaviorSubject, Observable } from "rxjs";
import { map } from "rxjs/operators";
const appSettings = require("application-settings");

@Injectable({ providedIn: "root" })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;

    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<User>(
            JSON.parse(appSettings.getString("currentUser", "[]"))
        );
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

    login(userName: string, password: string) : Observable<any> {

        return this.http
            .post<any>(`${environment.apiUrl}/api/user/authenticate`,
            {
                username: userName,
                password: password,
            })
            .pipe(
                map((user) => {
                    appSettings.setString("currentUser", JSON.stringify(user));
                    this.currentUserSubject.next(user);
                    return user;
                })
            );
    }

    logout() {
        appSettings.remove("currentUser");
        this.currentUserSubject.next(new User);
    }
}
