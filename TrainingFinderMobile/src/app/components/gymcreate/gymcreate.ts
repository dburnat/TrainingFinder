import { environment } from "./../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { Component, OnInit } from "@angular/core";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import * as app from "tns-core-modules/application";
import * as Toast from "nativescript-toast";
import { zip } from "rxjs";

@Component({
    selector: "gym",
    templateUrl: "gymcreate.html",
    styleUrls: ["gymcreate.css"],
})
export class GymCreateComponent implements OnInit {
    constructor(private http: HttpClient, private router: Router) {}

    ngOnInit(): void {}
    onDrawerButtonTap(): void {
        const sideDrawer = <RadSideDrawer>app.getRootView();
        sideDrawer.showDrawer();
    }

    createGym(
        gymName: string,
        street: string,
        number: string,
        zipCode: string,
        city: string
    ) {
        this.http
            .post(`${environment.apiUrl}/gym/create`, {
                gymName: gymName,
                street: street,
                number: number,
                zipCode: zipCode,
                city: city,
            })
            .subscribe(
                (result) => {
                    Toast.makeText(
                        "Gym created. We will moderate it now.",
                        "long"
                    ).show();
                    this.router.navigate(["login"]);
                },
                (error) => {
                    Toast.makeText(error.message, "long").show();
                    console.log(error);
                }
            );
    }
}
