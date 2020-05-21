import { AuthenticationService } from "./authentication.service";
import { ObservableArray } from "tns-core-modules/data/observable-array";
import { Training, TrainingAdapter } from "../models/training.model";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

import { environment } from "./../../environments/environment";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

@Injectable({ providedIn: "root" })
export class TrainingService {
    private userId: number;
    constructor(
        private http: HttpClient,
        private adapter: TrainingAdapter,
        private authenticationService: AuthenticationService
    ) {
        this.userId = this.authenticationService.currentUserValue.id;
    }

    getTrainingsForCurrentUser(): Observable<Training[]> {
        return this.http
            .get(`${environment.apiUrl}/api/traininguser/${this.userId}`)
            .pipe(
                map((data: any[]) =>
                    data.map((item) => this.adapter.adapt(item))
                )
            );
    }
}
