import { AuthenticationService } from "./authentication.service";
import { TrainingAdapter, Training } from "../models/training.model";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

import { environment } from "./../../environments/environment";
import { Observable, throwError } from "rxjs";
import { map, catchError } from "rxjs/operators";

@Injectable({ providedIn: "root" })
export class TrainingService {
    constructor(
        private http: HttpClient,
        private adapter: TrainingAdapter,
        private authenticationService: AuthenticationService
    ) {}

    getTrainingsForCurrentUser(): Observable<any> {
        return this.http
            .get(
                `${environment.apiUrl}/api/traininguser/${this.authenticationService.currentUserValue.id}`
            )
            .pipe(
                map((data: any[]) =>
                    data.map((item) => this.adapter.adapt(item))
                ),
                catchError((error): any => {
                    return throwError(`Connection Error: ${error}`);
                })
            );
    }
    getTrainingById(id: number): Observable<Training> {
        return this.http
            .get(`${environment.apiUrl}/api/training/${id}`)
            .pipe((data: any) => data.map((item) => this.adapter.adapt(item)));
    }

    getAllTrainingsFromApi(): Observable<any> {
        return this.http.get(`${environment.apiUrl}/api/training`).pipe(
            map((data: any[]) => data.map((item) => this.adapter.adapt(item))),
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

    createNewTrainingRequest(
        dateTime: string,
        description: string,
        gymId: number
    ): Observable<any> {
        return this.http.post<any>(`${environment.apiUrl}/api/training`, {
            description: description,
            dateTime: dateTime,
            gymId: gymId,
        });
    }
}
