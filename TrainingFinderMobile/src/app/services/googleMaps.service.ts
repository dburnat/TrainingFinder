import { Marker } from 'nativescript-google-maps-sdk';
import { Gym } from '~/app/models/gym.model';
import { MapView } from 'nativescript-google-maps-sdk';
import { ObservableArray } from "tns-core-modules/data/observable-array";
var mapsModule = require("nativescript-google-maps-sdk");
import { Injectable, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "~/environments/environment";
@Injectable({
    providedIn: "root",
})
export class googleMapsService {
    myGyms: ObservableArray<Gym>;
    constructor(private http: HttpClient) {
        this.OnInit();
    }

    OnInit(): void {
        this.getGymsFromApi().subscribe((data: any) => {
            this.myGyms = data;
        });
    }

    getGymsFromService(): ObservableArray<Gym> {
        return this.myGyms;
    }

    createMarkers(mapView: MapView, gyms: ObservableArray<Gym>): void{
        gyms.forEach(gym =>{
            let marker = new Marker();
            marker.position = mapsModule.Position.positionFromLatLng(gym.latitude,gym.longitude);
            var snippet = `${gym.street} ${gym.number}`;
            marker.title = gym.name;
            marker.snippet = snippet;
            marker.userData = {gymId: gym.gymId}
            mapView.addMarker(marker);
        })
    }

    private getGymsFromApi() {
        return this.http.get<Gym[]>(`${environment.apiUrl}/api/gym`);
    }
}
