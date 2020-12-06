import { registerModel } from "./../../models/register.model";
import { AuthenticationService } from "./../../services/authentication.service";
import { Component, OnInit } from "@angular/core";
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
    invalidUsername = false;
    invalidPassword = false;
    registerModel: registerModel;

    public constructor(
        private router: Router,
        private formBuilder: FormBuilder,
        private authenticationService: AuthenticationService
    ) {}

    ngOnInit(): void {
        this.initializeFormGroup();
    }

    private initializeFormGroup() {
        this.registerForm = this.formBuilder.group({
            username: ["", Validators.required],
            password: ["", Validators.required, Validators.min(6)],
        });
    }

    public onRegisterTap() {
        const usernameControl = this.registerForm.get("username");
        const passwordControl = this.registerForm.get("password");
        this.invalidPassword = passwordControl.invalid;
        this.invalidUsername = usernameControl.invalid;
        if (this.invalidPassword || this.invalidUsername) {
            let output = "";
            if (this.invalidUsername) {
                output += "Username is required";
            }
            if (this.invalidPassword) {
                if (output.length > 0) {
                    output += "\n";
                }
                output +=
                    "Password is required and should consist of minimum 6 characters";
            }
            Toast.makeText(output, "long").show();
            return;
        }
        const data = {
            username: usernameControl.value,
            password: passwordControl.value,
        };
        this.authenticationService.register(data).subscribe(
            () => {
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
