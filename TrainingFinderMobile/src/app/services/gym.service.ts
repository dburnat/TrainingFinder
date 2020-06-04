import { GymAdapter } from "./../models/gym.model";
import { AuthenticationService } from "./authentication.service";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

import { environment } from "./../../environments/environment";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Gym } from "../models/gym.model";

@Injectable({ providedIn: "root" })
export class GymService {
    private gyms: Observable<Gym[]>;
    constructor(private http: HttpClient, private adapter: GymAdapter) {
        this.OnInit();
    }

    OnInit(): void {
        this.getGymsFromApi().subscribe((data: any) => {
            this.gyms = data;
        });
    }

    getGymByIdFromApi(id: number): Observable<Gym> {
        return this.http
            .get(`${environment.apiUrl}/api/gym/id/${id}`)
            .pipe((data: any) => data.map((item) => this.adapter.adapt(item)));
    }

    getGymsFromApi(): Observable<Gym[]> {
        return this.http
            .get(`${environment.apiUrl}/api/gym`)
            .pipe(
                map((data: any[]) =>
                    data.map((item) => this.adapter.adapt(item))
                )
            );
    }

    createNewGym(
        gymName: string,
        street: string,
        number: string,
        city: string
    ): Observable<any> {
        return this.http.post(`${environment.apiUrl}/api/gym`, {
            name: gymName,
            city: city,
            street: street,
            number: number,
        });
    }
}
