import { getRootView } from "tns-core-modules/application/application";
import { GymService } from "./../../services/gym.service";
import { googleMapsService } from "./../../services/googleMaps.service";
import { Router } from "@angular/router";
import { AppDataService } from "./../../services/appdata.service";
import { userLocation } from "./../../models/userlocation.model";
import { AuthenticationService } from "./../../services/authentication.service";
import { Component, OnInit, DoCheck } from "@angular/core";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import * as app from "tns-core-modules/application";
import { Gym } from "~/app/models/gym.model";
import { CardView } from "nativescript-cardview";
import { registerElement } from "nativescript-angular/element-registry";
import { MapView } from "nativescript-google-maps-sdk";
import { Observable } from "rxjs";
import { TrainingService } from "~/app/services/training.service";

registerElement("CardView", () => CardView);
registerElement("MapView", () => MapView);

@Component({
    selector: "Home",
    templateUrl: "home.html",
    styleUrls: ["home.css"],
})
export class HomeComponent implements OnInit {
    constructor(
        public authenticationService: AuthenticationService,
        private appDataService: AppDataService,
        private router: Router,
        private gMapsService: googleMapsService,
        private gymService: GymService,
        private trainingService: TrainingService
    ) {
        // Use the component constructor to inject providers.
    }

    public user = this.authenticationService.currentUserValue;

    public gyms: Observable<Gym[]>;
    public userLocation: userLocation;
    private mapView: MapView;
    public trainingCounter: Number = 0;
    public drawer: RadSideDrawer;

    async ngOnInit(): Promise<void> {
        //this.page.actionBarHidden = true;
        await this.delay(1000);
        this.userLocation = this.appDataService.retrieveLocation();
        this.gymService.getGymsFromApi().subscribe((data: any) => {
            this.gyms = data;
        });
    }

    async onPageLoaded(): Promise<void> {
        await this.delay(500);
        this.trainingService
            .getTrainingsForCurrentUser()
            .subscribe((data: any) => {
                this.trainingCounter = data.length;
            });
    }

    ngAfterViewInit() {
        setTimeout(() => {
            this.drawer = <RadSideDrawer>getRootView();
            this.drawer.gesturesEnabled = true;
        }, 100);
    }

    delay(ms: number) {
        return new Promise((resolve) => setTimeout(resolve, ms));
    }

    onDrawerButtonTap(): void {
        const sideDrawer = <RadSideDrawer>app.getRootView();
        sideDrawer.showDrawer();
    }

    async onMapReady(args): Promise<void> {
        await this.delay(2000);
        this.mapView = args.object;

        this.mapView.latitude = this.userLocation.latitude;
        this.mapView.longitude = this.userLocation.longitude;
        this.mapView.zoom = 11;
        this.mapView.myLocationEnabled = true;
        this.mapView.settings.zoomGesturesEnabled = true;
        this.gMapsService.createMarkers(this.mapView, this.gyms);
    }

    onMarkerInfoWindowTapped(args) {
        let gymId = args.marker.userData.gymId;
        this.appDataService.saveGym(this.gyms[gymId - 1]);
        this.router.navigate(["gym"]);
    }

    async trainingClick() {
        await this.delay(300);
        this.router.navigate(["usersTrainings"]);
    }
}
