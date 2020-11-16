import { Component, OnInit } from "@angular/core";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import * as Toast from "nativescript-toast";
import * as ModalPicker from "nativescript-modal-datetimepicker";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { Gym } from "~/app/models/gym.model";
import { AppDataService } from "~/app/services/appdata.service";
import { TrainingService } from "~/app/services/training.service";
import { Router } from "@angular/router";
registerElement("CardView", () => CardView);
import { Application, EventData, ListPicker } from "@nativescript/core";
import { registerElement } from "@nativescript/angular";
import { CardView } from "@nstudio/nativescript-cardview";
const rootView = Application.getRootView();

@Component({
    selector: "ns-trainingcreate",
    templateUrl: "./trainingcreate.component.html",
    styleUrls: ["./trainingcreate.component.css"],
})
export class TrainingcreateComponent implements OnInit {
    public trainings: Array<string> = [
        " ",
        "Back",
        "Biceps",
        "Cardio",
        "Chest",
        "Full Body Workout",
        "Legs",
        "Pilates",
        "Triceps",
    ];

    newTrainingForm: FormGroup;
    public date: string;
    public time: string;
    gym: Gym;
    public description: string;
    constructor(
        private formBuilder: FormBuilder,
        private appDataService: AppDataService,
        private trainingService: TrainingService,
        private router: Router
    ) {}

    async ngOnInit(): Promise<void> {
        this.gym = this.appDataService.retrieveGym();
        this.newTrainingForm = this.formBuilder.group({
            date: ["", Validators.required],
            time: ["", Validators.required],
            description: ["", Validators.minLength(2)],
        });
        await this.delay(500);
    }

    delay(ms: number) {
        return new Promise((resolve) => setTimeout(resolve, ms));
    }
    pickDate() {
        const picker = new ModalPicker.ModalDatetimepicker();
        picker
            .pickDate({
                title: "Pick date",
                minDate: new Date(),
                is24HourView: true,
            })
            .then((result) => {
                this.date =
                    result["day"] +
                    "-" +
                    result["month"] +
                    "-" +
                    result["year"];
            })
            .catch((error) => {
                Toast.makeText(error, "long").show();
            });
    }

    pickTime() {
        const picker = new ModalPicker.ModalDatetimepicker();
        var date = new Date();
        picker
            .pickTime({
                title: "Pick time",
                theme: "dark",
                minDate: date,
                startingHour: date.getHours(),
                startingMinute: date.getMinutes(),
                is24HourView: true,
            })
            .then((result) => {
                this.time = result["hour"] + ":" + result["minute"];
            })
            .catch((error) => {
                Toast.makeText(error, "long").show();
            });
    }

    async addTrainingClick(): Promise<void> {
        let dateTime = this.time + " " + this.date;
        this.createNewTrainingRequest(
            dateTime,
            this.description,
            this.gym.gymId
        );
        await this.delay(500);
        this.router.navigate(["gym"]);
    }

    private createNewTrainingRequest(
        dateTime: string,
        description: string,
        gymId: number
    ) {
        if (dateTime === null || description === " " || gymId === null) return;
        this.trainingService
            .createNewTrainingRequest(
                dateTime,
                this.description,
                this.gym.gymId
            )
            .subscribe(
                () => {
                    Toast.makeText(
                        "Successfully added new training",
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

    public onSelectedIndexChanged(args: EventData) {
        const picker = <ListPicker>args.object;
        this.description = this.trainings[picker.selectedIndex];
    }

    onDrawerButtonTap(): void {
        const sideDrawer = <RadSideDrawer>rootView;
        sideDrawer.showDrawer();
    }
}
