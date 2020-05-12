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
import { MapView, Marker, Position } from "nativescript-google-maps-sdk";
import { isAndroid, isIOS } from "tns-core-modules/ui/page/page";
registerElement("CardView", () => CardView);
registerElement("MapView", () => MapView);
import * as geolocation from "nativescript-geolocation";
@Component({
    selector: "Home",
    templateUrl: "home.html",
})
export class HomeComponent implements OnInit {
    constructor(
        public authenticationService: AuthenticationService,
        private http: HttpClient,
        private appDataService: AppDataService
    ) {
        // Use the component constructor to inject providers.
    }
    public user = this.authenticationService.currentUserValue;
    public gyms: ObservableArray<Gym>;
    public userLocation: userLocation;

    ngOnInit(): void {
        this.userLocation = this.appDataService.retrieveLocation();
    }

    onDrawerButtonTap(): void {
        const sideDrawer = <RadSideDrawer>app.getRootView();
        sideDrawer.showDrawer();
    }

    getGymsFromApi() {
        return this.http.get<Gym[]>(`${environment.apiUrl}/api/gym`);
    }

    onMapReady = (event) => {
        let mapView = event.object as MapView;

        const POLAND_CENTER_LATITUDE = 52;
        const POLAND_CENTER_LONGITUDE = 19;

        mapView.latitude = POLAND_CENTER_LATITUDE;
        mapView.longitude = POLAND_CENTER_LONGITUDE;
        mapView.zoom = 5;
        mapView.myLocationEnabled = true;
        mapView.settings.zoomGesturesEnabled = true;
    };
}
