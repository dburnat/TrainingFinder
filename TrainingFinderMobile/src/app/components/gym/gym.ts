import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { RadSideDrawer } from 'nativescript-ui-sidedrawer';
import * as app from "tns-core-modules/application";

@Component({
    selector: 'gym',
    templateUrl: 'gym.html',
    styleUrls: ['gym.css']
})
export class GymComponent implements OnInit {
    constructor(private router: Router) { }

    ngOnInit(): void { }
    onDrawerButtonTap(): void {
        const sideDrawer = <RadSideDrawer>app.getRootView();
        sideDrawer.showDrawer();
    }

    gotoGymCreate() {
        this.router.navigate(["gymCreate"]);
    }
}
