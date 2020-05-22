import { AuthenticationService } from "./../../services/authentication.service";
import { TrainingService } from "./../../services/training.service";
import { Component, OnInit } from "@angular/core";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import * as app from "tns-core-modules/application";
import { Training } from "~/app/models/training.model";
import { Observable } from "rxjs";
import { registerElement } from "nativescript-angular/element-registry";
import { PullToRefresh } from "@nstudio/nativescript-pulltorefresh";
registerElement("PullToRefresh", () => PullToRefresh);
@Component({
    selector: "ns-userstrainings",
    templateUrl: "./userstrainings.component.html",
    styleUrls: ["./userstrainings.component.css"],
})
export class UsersTrainingsComponent implements OnInit {
    public trainings: Observable<Training[]>;

    constructor(
        private trainingService: TrainingService,
        private authenticationService: AuthenticationService
    ) {}

    async ngOnInit(): Promise<void> {
        await this.delay(500);
        this.trainingService
            .getTrainingsForCurrentUser()
            .subscribe((data: any) => {
                this.trainings = data;
            });
    }
    private delay(ms: number) {
        return new Promise((resolve) => setTimeout(resolve, ms));
    }

    onDrawerButtonTap(): void {
        const sideDrawer = <RadSideDrawer>app.getRootView();
        sideDrawer.showDrawer();
    }

    refreshList(args) {
        var pullRefresh = args.object;
        setTimeout(function () {
            pullRefresh.refreshing = false;
        }, 1000);
        this.trainingService
            .getTrainingsForCurrentUser()
            .subscribe((data: any) => {
                this.trainings = data;
            });
    }

    addTraining() {
        console.log("addtraining");
    }
}
