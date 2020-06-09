import { Training } from "./../../models/training.model";
import { Router } from "@angular/router";
import { AuthenticationService } from "./../../services/authentication.service";
import { TrainingService } from "./../../services/training.service";
import { Component, OnInit } from "@angular/core";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import * as app from "tns-core-modules/application";
import { Observable } from "rxjs";
import { registerElement } from "nativescript-angular/element-registry";
import { PullToRefresh } from "@nstudio/nativescript-pulltorefresh";
import { alert } from "tns-core-modules/ui/dialogs";
registerElement("PullToRefresh", () => PullToRefresh);
@Component({
    selector: "ns-userstrainings",
    templateUrl: "./userstrainings.component.html",
    styleUrls: ["./userstrainings.component.css"],
})
export class UsersTrainingsComponent implements OnInit {
    public trainings: Observable<Training[]>;
    private training: Training;

    constructor(
        private trainingService: TrainingService,
        private authenticationService: AuthenticationService,
        private router: Router
    ) {}

    async ngOnInit(): Promise<void> {
        await this.delay(500);
        this.trainingService
            .getTrainingsForCurrentUser()
            .subscribe((data: any) => {
                    this.trainings = data;
            },
            error =>{
                this.trainings= undefined;
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

    async viewInfo(id: number) {
        this.trainingService
            .getTrainingById(id)
            .subscribe(async (data: any) => {
                this.training = data;
            });

        await this.delay(200);
        let output = "";
        this.training.users.forEach((user: any) => {
            output += user.username + "\n";
        });
        let options = {
            title: "Users that also joined this training",
            message: output,
            okButtonText: "Okay",
        };
        alert(options).then(() => {});
    }

    goToGyms() {
        this.router.navigate(["gyms"]);
    }
}
