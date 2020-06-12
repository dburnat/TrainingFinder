import { Training } from "~/app/models/training.model";
import { GymService } from "../../services/gym.service";
import { TrainingService } from "../../services/training.service";
import { googleMapsService } from "../../services/googleMaps.service";
import { AuthenticationService } from "../../services/authentication.service";
import { AppDataService } from "../../services/appdata.service";
import { Router } from "@angular/router";
import { Component, OnInit, Injector } from "@angular/core";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import * as app from "tns-core-modules/application";
import { Gym } from "~/app/models/gym.model";
import { registerElement } from "nativescript-angular/element-registry";
import { CardView } from "nativescript-cardview";
import { confirm } from "tns-core-modules/ui/dialogs";
import * as Toast from "nativescript-toast";
import { MapView } from "nativescript-google-maps-sdk";
import { PullToRefresh } from "@nstudio/nativescript-pulltorefresh";
import { BasePage } from "~/app/helpers/base-page.decorator";
import { Subscription } from "rxjs";
registerElement("MapView", () => MapView);
registerElement("CardView", () => CardView);
registerElement("PullToRefresh", () => PullToRefresh);
@BasePage()
@Component({
    selector: "gym",
    templateUrl: "gym.component.html",
    styleUrls: ["gym.component.css"],
})
export class GymComponent implements OnInit {
    gymId: number;
    gym: Gym;
    private mapView: MapView;
    userTrainings: Training[] = [];
    private subscriptions: Subscription[] = [];

    constructor(
        private router: Router,
        private appDataService: AppDataService,
        private authenticationService: AuthenticationService,
        private gMapsService: googleMapsService,
        private trainingService: TrainingService,
        private gymService: GymService,
        private injector: Injector
    ) {}

    async ngOnInit(): Promise<void> {

        this.gym = this.appDataService.retrieveGym();
        this.subscriptions.push(
            this.trainingService
                .getTrainingsForCurrentUser()
                .subscribe((data: any) => {
                    this.userTrainings = data;
                })
        );
        await this.delay(500);
    }

    async ngOnDestroy(): Promise<void> {
        this.subscriptions.forEach((subscription) => subscription.unsubscribe());
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
        this.gymService
            .getGymByIdFromApi(this.gym.gymId)
            .subscribe((data: any) => {
                this.gym = data;
            });
    }

    checkIfJoined(id: Number): Boolean {
        for (let training of this.userTrainings) {
            if (training.trainingId == id) return false;
        }
        return true;
    }

    checkIfJoinedText(id: Number): String {
        for (let training of this.userTrainings) {
            if (training.trainingId == id) return "JOINED";
        }
        return "JOIN";
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
            if (result) this.joinTrainingRequest(id);
        });
    }

    private joinTrainingRequest(trainingId: number) {
        if (trainingId === null) return;

        this.trainingService
            .joinTrainingRequest(
                this.authenticationService.currentUserValue.id,
                trainingId
            )
            .subscribe(
                () => {
                    Toast.makeText(
                        "Joined training with id: " + trainingId,
                        "long"
                    ).show();
                },
                (error) => {
                    Toast.makeText(
                        "Something went wrong. Try again",
                        "long"
                    ).show();
                    console.log(error);
                }
            );
    }

    addTraining() {
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
