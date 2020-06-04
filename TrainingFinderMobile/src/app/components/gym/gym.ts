import { GymService } from "./../../services/gym.service";
import { TrainingService } from "./../../services/training.service";
import { googleMapsService } from "./../../services/googleMaps.service";
import { AuthenticationService } from "./../../services/authentication.service";
import { AppDataService } from "./../../services/appdata.service";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { Component, OnInit } from "@angular/core";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import * as app from "tns-core-modules/application";
import { Gym } from "~/app/models/gym.model";
import { registerElement } from "nativescript-angular/element-registry";
import { CardView } from "nativescript-cardview";
import { confirm } from "tns-core-modules/ui/dialogs";
import * as Toast from "nativescript-toast";
import { MapView } from "nativescript-google-maps-sdk";
import { PullToRefresh } from "@nstudio/nativescript-pulltorefresh";
registerElement("MapView", () => MapView);
registerElement("CardView", () => CardView);
registerElement("PullToRefresh", () => PullToRefresh);

@Component({
    selector: "gym",
    templateUrl: "gym.html",
    styleUrls: ["gym.css"],
})
export class GymComponent implements OnInit {
    gymId: number;
    gym: Gym;
    userId: number;
    isDataAvailable: boolean = false;
    private mapView: MapView;

    constructor(
        private http: HttpClient,
        private router: Router,
        private appDataService: AppDataService,
        private authenticationService: AuthenticationService,
        private gMapsService: googleMapsService,
        private trainingService: TrainingService,
        private gymService: GymService
    ) {}

    async ngOnInit(): Promise<void> {
        await this.delay(500);
        this.gym = this.appDataService.retrieveGym();
        this.userId = this.authenticationService.currentUserValue.id;
        this.isDataAvailable = true;
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
        this.gymService.getGymByIdFromApi(this.gym.gymId).subscribe((data: any) => {
            this.gym = data;
        });
    }

    joinTrainingClick(id: number) {
        let options = {
            title: "Join training",
            message:
                "Are you sure you want to join training with id: " + id + "?",
            okButtonText: "Yes",
            cancelButtonText: "No",
        };
        this.authenticationService.currentUserValue.id;
        confirm(options).then((result: boolean) => {
            this.joinTrainingRequest(this.userId, id);
        });
    }

    private joinTrainingRequest(userId: number, trainingId: number) {
        if (userId === null || trainingId === null) return;

        this.trainingService.joinTrainingRequest(userId, trainingId).subscribe(
            () => {
                Toast.makeText(
                    "Joined training with id: " + trainingId,
                    "long"
                ).show();
            },
            (error) => {
                Toast.makeText("Something went wrong. Try again", "long").show();
                console.log(error);
            }
        );
    }

    addTraining() {
        console.log("Create training button");
        this.router.navigate(["trainingCreate"]);
    }
    delay(ms: number) {
        return new Promise((resolve) => setTimeout(resolve, ms));
    }

    async onMapReady(args): Promise<void> {
        await this.delay(500);
        this.mapView = args.object;

        this.mapView.latitude = this.gym.latitude;
        this.mapView.longitude = this.gym.longitude;
        this.mapView.zoom = 15;
        this.mapView.settings.zoomGesturesEnabled = true;
        this.gMapsService.createMarker(this.mapView, this.gym);
    }
}
