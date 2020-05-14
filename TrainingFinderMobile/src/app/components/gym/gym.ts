import { googleMapsService } from './../../services/googleMaps.service';
import { AuthenticationService } from "./../../services/authentication.service";
import { AppDataService } from "./../../services/appdata.service";
import { environment } from "./../../../environments/environment";
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
registerElement("MapView", () => MapView);
registerElement("CardView", () => CardView);

@Component({
    selector: "gym",
    templateUrl: "gym.html",
    styleUrls: ["gym.css"],
})
export class GymComponent implements OnInit {
    gymId: string;
    gym: Gym;
    private sub: any;
    isDataAvailable: boolean = false;
    private mapView: MapView;

    constructor(
        private http: HttpClient,
        private router: Router,
        private appDataService: AppDataService,
        private authenticationService: AuthenticationService,
        private gMapsService: googleMapsService
    ) {}

    ngOnInit(): void {
        this.gym = this.appDataService.retrieveGym();
        this.isDataAvailable = true;
    }
    onDrawerButtonTap(): void {
        const sideDrawer = <RadSideDrawer>app.getRootView();
        sideDrawer.showDrawer();
    }

    getGym(id: string) {
        return new Promise(() => {
            this.getGymFromApi(id).subscribe((data: any) => {
                this.gym = data;
            });
        });
    }

    getGymFromApi(id: string) {
        return this.http.get<Gym>(
            `${environment.apiUrl}/api/gym/GymById/${id}`
        );
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
            console.log(result);
            console.log(
                "user id: " + this.authenticationService.currentUserValue.id
            );
            console.log("training id: " + id);
            this.joinTrainingRequest(this.authenticationService.currentUserValue.id, id);
        });
    }

    private joinTrainingRequest(userId: number, trainingId: number) {
        if(userId === null || trainingId === null) return;

        return this.http.post(`${environment.apiUrl}/api/TrainingUserApi`,
        {
            trainingId: trainingId,
            userId: userId
        })
        .subscribe(
            (result) => {
                Toast.makeText("Joined training with id: " + trainingId, "long").show();
            },
            (error) => {
                Toast.makeText(error.message, "long").show();
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
