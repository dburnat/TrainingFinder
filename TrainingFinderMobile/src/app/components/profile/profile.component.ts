import { environment } from "../../../environments/environment";
import { Component, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import * as Toast from "nativescript-toast";
import "rxjs/Rx";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";

@Component({
    selector: "profile",
    templateUrl: "profile.component.html",
    styleUrls: ["profile.component.css"],
})
export class ProfileComponent implements OnInit {
    registerForm: FormGroup;

    public constructor(
        private http: HttpClient,
        private router: Router,
        private formBuilder: FormBuilder
    ) {}

    async ngOnInit(): Promise<void> {
        this.registerForm = this.formBuilder.group({
            userName: ["", Validators.required],
            password: ["", Validators.required, Validators.minLength(6)],
        });
    }

    public register() {
        console.log(this.registerForm.get("username").valid);
        console.log("password", this.registerForm.get("password").valid);

        // this.http
        //     .post(`${environment.apiUrl}/api/user/register`, {
        //         firstName: firstName,
        //         lastName: lastName,
        //         username: userName,
        //         password: password,
        //     })
        //     .subscribe(
        //         (result) => {
        //             Toast.makeText("Account created", "long").show();
        //             this.router.navigate(["login"]);
        //         },
        //         (error) => {
        //             Toast.makeText(error.message, "long").show();
        //             console.log(error);
        //         }
        //     );
    }
}
