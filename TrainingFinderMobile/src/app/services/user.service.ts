import { UserProfile } from "./../models/user-profile.model";
import { User } from "../models/user.model";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

import { environment } from "./../../environments/environment";

@Injectable({ providedIn: "root" })
export class UserService {
    constructor(private http: HttpClient) {}

    getAll() {
        return this.http.get<User[]>(`${environment.apiUrl}/users`);
    }
    getById(id: number): Observable<UserProfile> {
        return this.http.get<UserProfile>(
            `${environment.apiUrl}/api/user/` + id
        );
    }
}
