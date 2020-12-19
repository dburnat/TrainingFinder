import { UserProfile } from "./../models/user-profile.model";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable, Subject } from "rxjs";

import { environment } from "./../../environments/environment";
import { AuthenticationService } from "./authentication.service";

@Injectable({ providedIn: "root" })
export class UserService {
    constructor(
        private http: HttpClient,
        private authenticationService: AuthenticationService
    ) {}
    private currentUserProfileSubject = new Subject<UserProfile>();

    public get currentUserProfile(): Observable<UserProfile> {
        return this.currentUserProfileSubject.asObservable();
    }
    public getCurrentUserProfile(): void {
        this.http
            .get<UserProfile>(
                `${environment.apiUrl}/api/user/` +
                    this.authenticationService.currentUserValue.id
            )
            .subscribe((data: UserProfile) => {
                this.currentUserProfileSubject.next(data);
            });
    }
}
