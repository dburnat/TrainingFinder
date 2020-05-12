import { userLocation } from "./../models/userlocation.model";
import { Injectable } from "@angular/core";
import { Gym } from "../models/gym.model";
@Injectable({
    providedIn: "root",
})
export class AppDataService {
    myGym: Gym;
    myLocation: userLocation;
    constructor() {}
    saveGym(gym: Gym) {
        this.myGym = gym;
    }
    retrieveGym() {
        return this.myGym;
    }

    saveLocation(userLocation: userLocation) {
        this.myLocation = userLocation;
    }

    retrieveLocation() {
        return this.myLocation;
    }
}
