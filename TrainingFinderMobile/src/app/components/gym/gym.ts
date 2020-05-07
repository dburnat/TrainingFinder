import { environment } from "./../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Router, ActivatedRoute, Params } from "@angular/router";
import { Component, OnInit, Input, OnDestroy } from "@angular/core";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import * as app from "tns-core-modules/application";
import { Gym } from "~/app/models/gym";
import { registerElement } from 'nativescript-angular/element-registry';
import { CardView } from 'nativescript-cardview';
import { concatMap } from "rxjs/operators";
import { of } from "rxjs";
registerElement('CardView', () => CardView);

@Component({
    selector: "gym",
    templateUrl: "gym.html",
    styleUrls: ["gym.css"],
})
export class GymComponent implements OnInit, OnDestroy {
    gymId: string;
    gym: Gym;
    private sub: any;
    isDataAvailable:boolean = false;

    constructor(
        private http: HttpClient,
        private router: Router,
        private route: ActivatedRoute
    ) {}

    ngOnInit(): void {
        this.sub = this.route.params.subscribe(
            (params: Params) => (this.gymId = params.id)
        );
        this.getGymFromApi(this.gymId).subscribe((data: any) =>{
            this.gym = data;
        });
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

    ngOnDestroy(): void {
        this.sub.unsubscribe();
    }
}
