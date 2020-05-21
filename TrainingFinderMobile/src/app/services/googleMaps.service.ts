import { Marker } from "nativescript-google-maps-sdk";
import { Gym } from "~/app/models/gym.model";
import { MapView } from "nativescript-google-maps-sdk";
var mapsModule = require("nativescript-google-maps-sdk");
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
@Injectable({
    providedIn: "root",
})
export class googleMapsService {
    constructor(private http: HttpClient) {
    }

    createMarker(mapView: MapView, gym): void {
        let marker = new Marker();
        marker.position = mapsModule.Position.positionFromLatLng(
            gym.latitude,
            gym.longitude
        );
        var snippet = `${gym.street} ${gym.number}`;
        marker.title = gym.name;
        marker.snippet = snippet;
        marker.userData = { gymId: gym.gymId };
        mapView.addMarker(marker);
    }

    createMarkers(mapView: MapView, gyms: Observable<Gym[]>): void {
        gyms.forEach((gym) => {
            let marker = new Marker();
            marker.position = mapsModule.Position.positionFromLatLng(
                gym.latitude,
                gym.longitude
            );
            var snippet = `${gym.street} ${gym.number}`;
            marker.title = gym.name;
            marker.snippet = snippet;
            marker.userData = { gymId: gym.gymId };
            mapView.addMarker(marker);
        });
    }
}
