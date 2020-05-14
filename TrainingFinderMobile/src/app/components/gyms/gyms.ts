import { googleMapsService } from "./../../services/googleMaps.service";
import { AppDataService } from "./../../services/appdata.service";
import { Gym } from "../../models/gym.model";
import { Router } from "@angular/router";
import { Component, OnInit } from "@angular/core";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import * as app from "tns-core-modules/application";
import { ObservableArray } from "tns-core-modules/data/observable-array";
import { CardView } from "nativescript-cardview";
import { registerElement } from "nativescript-angular/element-registry";
registerElement("CardView", () => CardView);
@Component({
    selector: "gym",
    templateUrl: "gyms.html",
    styleUrls: ["gyms.css"],
})
export class GymsComponent implements OnInit {
    constructor(
        private router: Router,
        private appDataService: AppDataService,
        private gMapsService: googleMapsService
    ) {}

    public gyms: ObservableArray<Gym>;
    token: string;

    async ngOnInit(): Promise<void> {
        await this.delay(500);
        this.gyms = this.gMapsService.getGymsFromService();
        console.log(this.gyms);
    }
    private delay(ms: number) {
        return new Promise((resolve) => setTimeout(resolve, ms));
    }

    onDrawerButtonTap(): void {
        const sideDrawer = <RadSideDrawer>app.getRootView();
        sideDrawer.showDrawer();
    }

    goToGymCreate() {
        this.router.navigate(["gymCreate"]);
    }
    goToGym(gymId: number): void {
        console.log("Gyms view: " + gymId);
        this.appDataService.saveGym(this.gyms[gymId - 1]);
        this.router.navigate(["gym"]);
    }
}
