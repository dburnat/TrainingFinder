// import { environment } from './../../../environments/environment';
// import { Component, OnInit } from "@angular/core";
// import { HttpClient, HttpHeaders, HttpRequest } from "@angular/common/http";
// import { ActivatedRoute } from "@angular/router";
// import "rxjs/Rx";

// @Component({
//     selector: "home",
//     templateUrl: "home.html",
// })
// export class HomeComponent implements OnInit {

//     public mainContentText: string;

//     public constructor(private http: HttpClient, private route: ActivatedRoute) {
//         // this.content = "";
//     }

//     public ngOnInit() {
//         this.mainContentText = "Home screen"
//         // this.route.queryParams.subscribe(params => {
//         //     let headers = new HttpHeaders({ "Authorization": "Basic " + params["jwt"] });
//         //     this.http.get(`${environment.apiUrl}/users`,{ headers: headers })
//         //         .subscribe(result => {
//         //             this.content = result.toString();
//         //         });
//         // });
//     }



// }

import { Component, OnInit } from "@angular/core";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import * as app from "tns-core-modules/application";

@Component({
    selector: "Home",
    templateUrl: "home.html"
    
})
export class HomeComponent implements OnInit {

    constructor() {
        // Use the component constructor to inject providers.
    }

    ngOnInit(): void {
        // Init your component properties here.
    }

    onDrawerButtonTap(): void {
        const sideDrawer = <RadSideDrawer>app.getRootView();
        sideDrawer.showDrawer();
    }
}
