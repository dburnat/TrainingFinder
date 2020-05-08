import { AppDataService } from './../../services/appdata.service';
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { Gym } from "../../models/gym.model";
import { Router } from "@angular/router";
import { Component, OnInit } from "@angular/core";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import * as app from "tns-core-modules/application";
import { ObservableArray } from "tns-core-modules/data/observable-array";
import { CardView } from 'nativescript-cardview';
import { registerElement } from 'nativescript-angular/element-registry';
registerElement('CardView', () => CardView);
@Component({
    selector: "gym",
    templateUrl: "gyms.html",
    styleUrls: ["gyms.css"],
})
export class GymsComponent implements OnInit {
    constructor(private router: Router, private http: HttpClient, private appDataService : AppDataService) {}

    public gyms: ObservableArray<Gym>;
    token: string;

    ngOnInit(): void {
        this.getGymsFromApi().subscribe((data: any) => {
            this.gyms = data;
        });
    }
    onDrawerButtonTap(): void {
        const sideDrawer = <RadSideDrawer>app.getRootView();
        sideDrawer.showDrawer();
    }

    getGymsFromApi() {
        return this.http.get<Gym[]>(`${environment.apiUrl}/api/gym`);
    }

    goToGymCreate() {
        this.router.navigate(["gymCreate"]);
    }
    goToGym(gymId: number): void{
        console.log("Gyms view: " + gymId);
        this.appDataService.saveGym(this.gyms[gymId - 1]);
        this.router.navigate(["gym"]);
    }
}
