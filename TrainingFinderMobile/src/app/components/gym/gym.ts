import { environment } from "./../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Router, ActivatedRoute, Params } from "@angular/router";
import { Component, OnInit, Input, OnDestroy } from "@angular/core";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import * as app from "tns-core-modules/application";
import * as Toast from "nativescript-toast";
import { zip } from "rxjs";
import { Gym } from "~/app/models/gym";

@Component({
    selector: "gym",
    templateUrl: "gym.html",
    styleUrls: ["gym.css"],
})
export class GymComponent implements OnInit, OnDestroy {
    gymId: string;
    gym: Gym;
    private sub: any;

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
        })
    }
    onDrawerButtonTap(): void {
        const sideDrawer = <RadSideDrawer>app.getRootView();
        sideDrawer.showDrawer();
    }


    getGymFromApi(id:string){
        return this.http.get<Gym>(`${environment.apiUrl}/api/gym/GymById/${id}`);
    }

    ngOnDestroy(): void {
        this.sub.unsubscribe();
    }
}
