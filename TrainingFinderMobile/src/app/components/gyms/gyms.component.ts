import { TrainingService } from "~/app/services/training.service";
import { GymService } from "../../services/gym.service";
import { AppDataService } from "../../services/appdata.service";
import { Gym } from "../../models/gym.model";
import { Router } from "@angular/router";
import { Component, OnInit, Injector } from "@angular/core";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import * as app from "tns-core-modules/application";
import { CardView } from "nativescript-cardview";
import { registerElement } from "nativescript-angular/element-registry";
import { PullToRefresh } from "@nstudio/nativescript-pulltorefresh";
import { Observable, Subscription } from "rxjs";
import { BasePage } from "~/app/helpers/base-page.decorator";
registerElement("CardView", () => CardView);
registerElement("PullToRefresh", () => PullToRefresh);
@BasePage()
@Component({
    selector: "gym",
    templateUrl: "gyms.component.html",
    styleUrls: ["gyms.component.css"],
})
export class GymsComponent implements OnInit {
    constructor(
        private router: Router,
        private appDataService: AppDataService,
        private gymService: GymService,
        private TrainingService: TrainingService,
        private injector: Injector
    ) {}

    public gyms: Observable<Gym[]>;
    public gymCounter: Number;
    public trainingCounter: Number;
    token: string;
    private subscriptions: Subscription[] = [];

    async ngOnInit(): Promise<void> {
        this.subscriptions.push(
            this.gymService.getGymsFromApi().subscribe((data: any) => {
                this.gyms = data;
            })
        );
        this.subscriptions.push(
            this.gymService.getGymsFromApi().subscribe((data: any) => {
                this.gymCounter = data.length;
            })
        );
        this.subscriptions.push(
            this.TrainingService.getAllTrainingsFromApi().subscribe(
                (data: any) => {
                    this.trainingCounter = data.length;
                }
            )
        );
        await this.delay(500);
    }

    async ngOnDestroy(): Promise<void> {
        this.subscriptions.forEach((subscription) => subscription.unsubscribe());
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
        this.appDataService.saveGym(this.gyms[gymId - 1]);
        this.router.navigate(["gym"]);
    }
}
