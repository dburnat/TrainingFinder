import { Component, OnInit } from "@angular/core";
import { HttpClient, HttpHeaders, HttpRequest } from "@angular/common/http";
import { ActivatedRoute } from "@angular/router";
import "rxjs/Rx";

@Component({
    selector: "home",
    templateUrl: "home.html",
})
export class HomeComponent implements OnInit {

    public content: string;

    public constructor(private http: HttpClient, private route: ActivatedRoute) {
        this.content = "";
    }

    public ngOnInit() {
        // this.route.queryParams.subscribe(params => {
        //     let headers = new HttpHeaders({ "Authorization": "Basic " + params["jwt"] });
        //     let options = new HttpRequest({ headers: headers });
        //     this.http.get("https://localhost:4000/users", options)
        //         .subscribe(result => {
        //             this.content = result.message;
        //         });
        // });
    }

}
