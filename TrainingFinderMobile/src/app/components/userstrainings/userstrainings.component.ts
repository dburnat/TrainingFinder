import { ObservableArray } from "tns-core-modules/data/observable-array";
import { Component, OnInit } from "@angular/core";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import * as app from "tns-core-modules/application";
import { Training } from "~/app/models/training.model";

@Component({
    selector: "ns-userstrainings",
    templateUrl: "./userstrainings.component.html",
    styleUrls: ["./userstrainings.component.css"],
})
export class UsersTrainingsComponent implements OnInit {
    trainings: ObservableArray<Training>;

    constructor() {}

    async ngOnInit(): Promise<void> {
        await this.delay(500);
    }
    private delay(ms: number) {
        return new Promise((resolve) => setTimeout(resolve, ms));
    }

    onDrawerButtonTap(): void {
        const sideDrawer = <RadSideDrawer>app.getRootView();
        sideDrawer.showDrawer();
    }
}
