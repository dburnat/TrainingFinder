import { AuthenticationService } from "./authentication.service";
import { TrainingAdapter } from "../models/training.model";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

import { environment } from "./../../environments/environment";
import { Observable, throwError } from "rxjs";
import { map, catchError } from "rxjs/operators";

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

    // OnInit(): void {
    //     this.gymService.getGyms().subscribe((data: any) => {
    //         this.myGyms = data;
    //     });
    // }

    getTrainingsForCurrentUser(): Observable<any> {
        return this.http
            .get(`${environment.apiUrl}/api/traininguser/${this.userId}`)
            .pipe(
                map((data: any[]) =>
                    data.map((item) => this.adapter.adapt(item))
                ),
                catchError((error): any => {
                    return throwError(`Connection Error: ${error}`);
                })
            );
    }

    joinTrainingRequest(
        userId: number,
        trainingId: number
    ): Observable<Object> {
        return this.http.post(`${environment.apiUrl}/api/traininguser`, {
            trainingId: trainingId,
            userId: userId,
        });
    }
}
