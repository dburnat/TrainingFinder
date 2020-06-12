import { GymService } from "../../services/gym.service";
import { Router } from "@angular/router";
import { Component, OnInit } from "@angular/core";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import * as app from "tns-core-modules/application";
import * as Toast from "nativescript-toast";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";

@Component({
    selector: "gym",
    templateUrl: "gymcreate.component.html",
    styleUrls: ["gymcreate.component.css"],
})
export class GymCreateComponent implements OnInit {
    newGymForm: FormGroup;

    constructor(
        private router: Router,
        private gymService: GymService,
        private formBuilder: FormBuilder
    ) {}

    async ngOnInit(): Promise<void> {
        this.newGymForm = this.formBuilder.group({
            gymName: ["", Validators.required],
            street: ["", Validators.required],
            number: ["", Validators.required],
            city: ["", Validators.required],
        });
        await this.delay(500);
    }
    onDrawerButtonTap(): void {
        const sideDrawer = <RadSideDrawer>app.getRootView();
        sideDrawer.showDrawer();
    }

    async createGymClick(
        gymName: string,
        street: string,
        number: string,
        city: string
    ): Promise<void> {
        this.createGymRequest(gymName, street, number, city);
        await this.delay(500);
        this.router.navigate(["home"]);
    }

    delay(ms: number) {
        return new Promise((resolve) => setTimeout(resolve, ms));
    }

    private createGymRequest(
        gymName: string,
        street: string,
        number: string,
        city: string
    ) {
        if (
            gymName === null ||
            street === null ||
            number === null ||
            city === null
        )
            return;
        this.gymService.createNewGym(gymName, street, number, city).subscribe(
            () => {
                Toast.makeText(
                    "Gym created. We will moderate it now.",
                    "long"
                ).show();
            },
            (error) => {
                Toast.makeText(
                    "Something went wrong. Try again",
                    "long"
                ).show();
            }
        );
    }
}
