import { AuthenticationService } from './../../services/authentication.service';
import { Component, OnInit } from "@angular/core";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import * as app from "tns-core-modules/application";

@Component({
    selector: "Home",
    templateUrl: "home.html"

})
export class HomeComponent implements OnInit {

    constructor(public authenticationService: AuthenticationService) {
        // Use the component constructor to inject providers.
    }
    public user = this.authenticationService.currentUserValue;

    ngOnInit(): void {
    }

    onDrawerButtonTap(): void {
        const sideDrawer = <RadSideDrawer>app.getRootView();
        sideDrawer.showDrawer();
    }
}
