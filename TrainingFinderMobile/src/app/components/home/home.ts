import { HttpClient } from "@angular/common/http";
import { AuthenticationService } from "./../../services/authentication.service";
import { Component, OnInit } from "@angular/core";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import * as app from "tns-core-modules/application";
import { environment } from "~/environments/environment";
import { Gym } from "~/app/models/gym.model";
import { ObservableArray } from "tns-core-modules/data/observable-array/observable-array";
import { CardView } from 'nativescript-cardview';
import { registerElement } from 'nativescript-angular/element-registry';
registerElement('CardView', () => CardView);


@Component({
    selector: "Home",
    templateUrl: "home.html",
})
export class HomeComponent implements OnInit {
    constructor(
        public authenticationService: AuthenticationService,
        private http: HttpClient
    ) {
        // Use the component constructor to inject providers.
    }
    public user = this.authenticationService.currentUserValue;
    public gyms: ObservableArray<Gym>;

    ngOnInit(): void {}

    onDrawerButtonTap(): void {
        const sideDrawer = <RadSideDrawer>app.getRootView();
        sideDrawer.showDrawer();
    }

    getGymsFromApi() {
        return this.http.get<Gym[]>(`${environment.apiUrl}/api/gym`);
    }
}
