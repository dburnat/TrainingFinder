import { Component, OnInit } from "@angular/core";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import * as app from "tns-core-modules/application";
import { EventData } from "tns-core-modules/data/observable";
import { ListPicker } from "tns-core-modules/ui/list-picker";
import { DatePicker } from "tns-core-modules/ui/date-picker";
import { TimePicker } from "tns-core-modules/ui/time-picker";
import { CardView } from "nativescript-cardview";
import { registerElement } from "nativescript-angular/element-registry";
registerElement("CardView", () => CardView);

@Component({
    selector: "ns-trainingcreate",
    templateUrl: "./trainingcreate.component.html",
    styleUrls: ["./trainingcreate.component.css"],
})
export class TrainingcreateComponent implements OnInit {
    public trainings: Array<string> = [
        "Full Body Workout",
        "Cardio",
        "Klatka",
        "Plecy",
    ];
    minDate: Date = new Date(2020, 4, 8);
    maxDate: Date = new Date(2045, 4, 12);
    constructor() {}

    ngOnInit(): void {}

    public onSelectedIndexChanged(args: EventData) {
        const picker = <ListPicker>args.object;
        console.log(
            `index: ${picker.selectedIndex}; item" ${
                this.trainings[picker.selectedIndex]
            }`
        );
    }

    onDatePickerLoaded(args) {
        // const datePicker = args.object as DatePicker;
    }

    onTimeChanged(args) {
        const tp = args.object as TimePicker;

        const time = args.value;
        console.log(`Chosen time: ${time}`);
    }

    onDateChanged(args) {
        console.log("Date New value: " + args.value);
        console.log("Date value: " + args.oldValue);
    }

    onDayChanged(args) {
        console.log("Day New value: " + args.value);
        console.log("Day Old value: " + args.oldValue);
    }

    onMonthChanged(args) {
        console.log("Month New value: " + args.value);
        console.log("Month Old value: " + args.oldValue);
    }

    onYearChanged(args) {
        console.log("Year New value: " + args.value);
        console.log("Year Old value: " + args.oldValue);
    }

    onDrawerButtonTap(): void {
        const sideDrawer = <RadSideDrawer>app.getRootView();
        sideDrawer.showDrawer();
    }
}
