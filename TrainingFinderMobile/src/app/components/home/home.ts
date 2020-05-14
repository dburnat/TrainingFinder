import { googleMapsService } from "./../../services/googleMaps.service";
import { Router } from "@angular/router";
import { AppDataService } from "./../../services/appdata.service";
import { userLocation } from "./../../models/userlocation.model";
import { HttpClient } from "@angular/common/http";
import { AuthenticationService } from "./../../services/authentication.service";
import { Component, OnInit } from "@angular/core";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import * as app from "tns-core-modules/application";
import { environment } from "~/environments/environment";
import { Gym } from "~/app/models/gym.model";
import { ObservableArray } from "tns-core-modules/data/observable-array/observable-array";
import { CardView } from "nativescript-cardview";
import { registerElement } from "nativescript-angular/element-registry";
import { MapView } from "nativescript-google-maps-sdk";
registerElement("CardView", () => CardView);
registerElement("MapView", () => MapView);

@Component({
    selector: "Home",
    templateUrl: "home.html",
})
export class HomeComponent implements OnInit {
    constructor(
        public authenticationService: AuthenticationService,
        private http: HttpClient,
        private appDataService: AppDataService,
        private router: Router,
        private gMapsService: googleMapsService
    ) {
        // Use the component constructor to inject providers.
    }
    public user = this.authenticationService.currentUserValue;

    public gyms: ObservableArray<Gym>;
    public userLocation: userLocation;
    private mapView: MapView;
    private com: any;

    async ngOnInit(): Promise<void> {
        await this.delay(1000);
        this.userLocation = this.appDataService.retrieveLocation();
        this.gyms = this.gMapsService.getGymsFromService();
    }

    delay(ms: number) {
        return new Promise((resolve) => setTimeout(resolve, ms));
    }

    onDrawerButtonTap(): void {
        const sideDrawer = <RadSideDrawer>app.getRootView();
        sideDrawer.showDrawer();
    }

    getGymsFromApi() {
        return this.http.get<Gym[]>(`${environment.apiUrl}/api/gym`);
    }

    async onMapReady(args): Promise<void> {
        await this.delay(1000);
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
}
