import { environment } from "../../../environments/environment";
import { Component, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import * as Toast from "nativescript-toast";
import "rxjs/Rx";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";

@Component({
    selector: "register",
    templateUrl: "register.component.html",
    styleUrls: ["register.component.css"],
})
export class RegisterComponent implements OnInit {
    registerForm: FormGroup;

    public constructor(
        private http: HttpClient,
        private router: Router,
        private formBuilder: FormBuilder
    ) {}

    async ngOnInit(): Promise<void> {
        this.registerForm = this.formBuilder.group({
            userName: ["", Validators.required],
            password: ["", Validators.required],
            firstName: ["", Validators.required],
            lastName: ["", Validators.required],
        });
    }

    public register(
        userName: string,
        firstName: string,
        lastName: string,
        password: string
    ) {
        this.http
            .post(`${environment.apiUrl}/api/user/register`, {
                firstName: firstName,
                lastName: lastName,
                username: userName,
                password: password,
            })
            .subscribe(
                (result) => {
                    Toast.makeText("Account created", "long").show();
                    this.router.navigate(["login"]);
                },
                (error) => {
                    Toast.makeText(error.message, "long").show();
                    console.log(error);
                }
            );
    }
}
