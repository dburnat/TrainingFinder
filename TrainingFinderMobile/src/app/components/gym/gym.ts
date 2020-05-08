import { AppDataService } from './../../services/appdata.service';
import { environment } from "./../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { Component, OnInit } from "@angular/core";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import * as app from "tns-core-modules/application";
import { Gym } from "~/app/models/gym.model";
import { registerElement } from 'nativescript-angular/element-registry';
import { CardView } from 'nativescript-cardview';
import { confirm } from "tns-core-modules/ui/dialogs";

registerElement('CardView', () => CardView);

@Component({
    selector: "gym",
    templateUrl: "gym.html",
    styleUrls: ["gym.css"],
})
export class GymComponent implements OnInit {
    gymId: string;
    gym: Gym;
    private sub: any;
    isDataAvailable:boolean = false;

    constructor(
        private http: HttpClient,
        private router: Router,
        private appDataService: AppDataService
    ) {}

    ngOnInit(): void {
        this.gym = this.appDataService.retrieveGym();
        this.isDataAvailable = true;
    }
    onDrawerButtonTap(): void {
        const sideDrawer = <RadSideDrawer>app.getRootView();
        sideDrawer.showDrawer();
    }

    getGym(id:string){
        return new Promise(() => {
            this.getGymFromApi(id).subscribe((data: any) =>{
                this.gym = data;
            });
        })
    }

    getGymFromApi(id:string){
        return this.http.get<Gym>(`${environment.apiUrl}/api/gym/GymById/${id}`);
    }

    joinTraining(id:string){

        let options = {
            title: "Join training",
            message: "Are you sure you want to join training with id: " + id + "?",
            okButtonText: "Yes",
            cancelButtonText: "No",
        };

        confirm(options).then((result: boolean) => {
            console.log(result);
        });
    }

    addTraining(){
        console.log("Create training button");
        this.router.navigate(["trainingCreate"]);
    }
}
