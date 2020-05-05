import { HttpClient } from "@angular/common/http";
import { environment } from "./../../../environments/environment";
import { Gym } from "./../../models/gym";
import { Router } from "@angular/router";
import { Component, OnInit } from "@angular/core";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import * as app from "tns-core-modules/application";
import { ObservableArray } from "tns-core-modules/data/observable-array";

@Component({
    selector: "gym",
    templateUrl: "gym.html",
    styleUrls: ["gym.css"],
})
export class GymComponent implements OnInit {
    constructor(private router: Router, private http: HttpClient) {}

    public gyms: ObservableArray<Gym>;
    token: string;

    ngOnInit(): void {
        this.getGymsFromApi().subscribe((data: any) => {
            this.gyms = data;
            console.log(this.gyms);
        });
    }
    onDrawerButtonTap(): void {
        const sideDrawer = <RadSideDrawer>app.getRootView();
        sideDrawer.showDrawer();
    }

    getGymsFromApi() {
        return this.http.get<Gym[]>(`${environment.apiUrl}/api/gym`);
    }

    gotoGymCreate() {
        this.router.navigate(["gymCreate"]);
    }
}
