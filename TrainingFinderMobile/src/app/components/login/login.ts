import { AppDataService } from "./../../services/appdata.service";
import { AuthenticationService } from "./../../services/authentication.service";
import { Router, ActivatedRoute } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { first } from "rxjs/operators";
import * as geolocation from "nativescript-geolocation";
import { Accuracy } from "tns-core-modules/ui/enums";

@Component({
    selector: "login",
    templateUrl: "login.html",
})
export class LoginComponent implements OnInit {
    public constructor(
        private router: Router,
        private authenticationService: AuthenticationService,
        private appDataService: AppDataService
    ) {
        // redirect to home if already logged in
        if (this.authenticationService.currentUserValue) {
            this.router.navigate(["/"]);
        }
    }

    public login(userName: string, password: string) {
        if (userName === null || password === null) return;

        return this.authenticationService
            .login(userName, password)
            .pipe(first())
            .subscribe(
                (result) => {
                    this.router.navigate(["home"]);
                },
                (error) => {
                    console.log(error);
                    // this.loading = false;
                }
            );
    }
    public goToRegister() {
        this.router.navigate(["register"]);
    }

    private getLocationPermission() {
        geolocation.isEnabled().then(
            function (isEnabled) {
                if (!isEnabled) {
                    geolocation
                        .enableLocationRequest(true, true)
                        .then(
                            () => {
                                console.log("User Enabled Location Service");
                            },
                            (e) => {
                                console.log("Error: " + (e.message || e));
                            }
                        )
                        .catch((ex) => {
                            console.log("Unable to Enable Location", ex);
                        });
                }
            },
            function (e) {
                console.log("Error: " + (e.message || e));
            }
        );
    }

    private saveCurrentLocation() {
        let that = this;
        geolocation
            .getCurrentLocation({
                desiredAccuracy: Accuracy.high,
                maximumAge: 5000,
                timeout: 10000,
            })
            .then(
                function (loc) {
                    if (loc) {
                        that.appDataService.saveLocation(loc);
                    }
                },
                function (e) {
                    console.log("Error: " + (e.message || e));
                }
            );
    }

    ngOnInit(): void {
        this.getLocationPermission();
        this.saveCurrentLocation();
    }
}
