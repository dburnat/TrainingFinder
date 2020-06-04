import { TrainingService } from '~/app/services/training.service';
import { GymService } from './../../services/gym.service';
import { AppDataService } from "./../../services/appdata.service";
import { Gym } from "../../models/gym.model";
import { Router } from "@angular/router";
import { Component, OnInit } from "@angular/core";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import * as app from "tns-core-modules/application";
import { CardView } from "nativescript-cardview";
import { registerElement } from "nativescript-angular/element-registry";
import { PullToRefresh } from "@nstudio/nativescript-pulltorefresh";
import { Observable } from 'rxjs';
registerElement("CardView", () => CardView);
registerElement("PullToRefresh", () => PullToRefresh);
@Component({
    selector: "gym",
    templateUrl: "gyms.html",
    styleUrls: ["gyms.css"],
})
export class GymsComponent implements OnInit {
    constructor(
        private router: Router,
        private appDataService: AppDataService,
        private gymService: GymService,
        private TrainingService: TrainingService
    ) {}

    public gyms: Observable<Gym[]>;
    public gymCounter: Number;
    public trainingCounter: Number;
    token: string;

    async ngOnInit(): Promise<void> {
        this.gymService.getGymsFromApi().subscribe((data: any) => {
            this.gyms = data;
        });

        await this.delay(500);
    }

    async onPageLoaded(): Promise<void> {
        this.gymService.getGymsFromApi().subscribe((data: any) => {
            this.gymCounter = data.length;
        });
        this.TrainingService.getAllTrainingsFromApi().subscribe((data: any) => {
            this.trainingCounter = data.length;
        });
        await this.delay(500);
    }
    private delay(ms: number) {
        return new Promise((resolve) => setTimeout(resolve, ms));
    }

    onDrawerButtonTap(): void {
        const sideDrawer = <RadSideDrawer>app.getRootView();
        sideDrawer.showDrawer();
    }
    refreshList(args) {
        var pullRefresh = args.object;
        setTimeout(function () {
            pullRefresh.refreshing = false;
        }, 1000);
        this.gymService.getGymsFromApi().subscribe((data: any) => {
            this.gyms = data;
        });
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
