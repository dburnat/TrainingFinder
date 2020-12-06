import { GymService } from "../../services/gym.service";
import { googleMapsService } from "../../services/googleMaps.service";
import { Router } from "@angular/router";
import { AppDataService } from "../../services/appdata.service";
import { userLocation } from "../../models/userlocation.model";
import { AuthenticationService } from "../../services/authentication.service";
import { Component, Injector } from "@angular/core";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import { Gym } from "~/app/models/gym.model";
import { MapView } from "nativescript-google-maps-sdk";
import { Observable, Subscription } from "rxjs";
import { TrainingService } from "~/app/services/training.service";
import { Training } from "~/app/models/training.model";
import { BasePage } from "~/app/helpers/base-page.decorator";
import { registerElement } from "@nativescript/angular";
import { getRootView } from "@nativescript/core/application";
import { CardView } from "@nstudio/nativescript-cardview";

registerElement("CardView", () => CardView);
registerElement("MapView", () => MapView);

@BasePage()
@Component({
    selector: "Home",
    templateUrl: "home.component.html",
    styleUrls: ["home.component.css"],
})
export class HomeComponent {
    constructor(
        public authenticationService: AuthenticationService,
        private appDataService: AppDataService,
        private router: Router,
        private gMapsService: googleMapsService,
        private gymService: GymService,
        private trainingService: TrainingService,
        private injector: Injector
    ) {
        // Use the component constructor to inject providers.
    }

    public user = this.authenticationService.currentUserValue;

    public gyms: Observable<Gym[]>;
    public trainings: Training[] = [];
    public userLocation: userLocation;
    private mapView: MapView;
    public trainingCounter: Number = 0;
    public drawer: RadSideDrawer;
    private subscriptions: Subscription[] = [];

    async ngOnInit(): Promise<void> {
        //this.page.actionBarHidden = true;
        await this.delay(1000);
        this.userLocation = await this.appDataService.retrieveLocation();
        this.subscriptions.push(
            this.trainingService.getTrainingsForCurrentUser().subscribe(
                (data: any) => {
                    this.trainings = data;
                    this.trainingCounter = this.trainings.length;
                },
                (error) => {
                    this.trainings = [];
                }
            )
        );
        this.subscriptions.push(
            this.gymService.getGymsFromApi().subscribe(async (data: any) => {
                this.gyms = await data;
            })
        );
    }

    async ngOnDestroy(): Promise<void> {
        this.subscriptions.forEach((subscription) =>
            subscription.unsubscribe()
        );
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
        const sideDrawer = <RadSideDrawer>getRootView();
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
        await this.gMapsService.createMarkers(this.mapView, this.gyms);
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
