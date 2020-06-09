import { userLocation } from "./../models/userlocation.model";
import { Injectable } from "@angular/core";
import { Gym } from "../models/gym.model";
import * as geolocation from "nativescript-geolocation";
import { Accuracy } from "tns-core-modules/ui/enums";
@Injectable({
    providedIn: "root",
})
export class AppDataService {
    myGym: Gym;
    myLocation: userLocation;
    appDataService: any;
    constructor() {
        this.OnInit();
    }
OnInit(): void{
    this.getLocationPermission();
    this.saveCurrentLocation();
    this.appDataService = this;
}

    saveGym(gym: Gym) {
        this.myGym = gym;
    }
    retrieveGym() {
        return this.myGym;
    }

    saveLocation(userLocation: userLocation) {
        this.myLocation = userLocation;
    }

    async retrieveLocation() {
        return this.myLocation;
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
}
