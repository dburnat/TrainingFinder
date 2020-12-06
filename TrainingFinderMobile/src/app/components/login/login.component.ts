import { AuthenticationService } from "../../services/authentication.service";
import { Router } from "@angular/router";
import { Component } from "@angular/core";
import * as Toast from "nativescript-toast";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { getRootView } from "@nativescript/core/application";
@Component({
    selector: "login",
    templateUrl: "login.component.html",
    styleUrls: ["login.component.css"],
})
export class LoginComponent {
    public drawer: RadSideDrawer;
    public loginForm: FormGroup;
    invalidUsername = false;
    invalidPassword = false;

    public constructor(
        private router: Router,
        private authenticationService: AuthenticationService,
        private formBuilder: FormBuilder
    ) {
        // redirect to home if already logged in
        if (this.authenticationService.currentUserValue) {
            this.router.navigate(["/"]);
        }
    }

    ngOnInit(): void {
        this.initializeFormGroup();
    }

    private initializeFormGroup() {
        this.loginForm = this.formBuilder.group({
            username: ["", Validators.required],
            password: ["", Validators.required],
        });
    }

    delay(ms: number) {
        return new Promise((resolve) => setTimeout(resolve, ms));
    }

    ngAfterViewInit() {
        setTimeout(() => {
            this.drawer = <RadSideDrawer>getRootView();
            this.drawer.gesturesEnabled = false;
        }, 100);
    }

    public onLoginTap() {
        const usernameControl = this.loginForm.get("username");
        const passwordControl = this.loginForm.get("password");
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
                output += "Password is required";
            }
            Toast.makeText(output, "long").show();
            return;
        }
        const data = {
            username: usernameControl.value,
            password: passwordControl.value,
        };

        return this.authenticationService.login(data).subscribe(
            () => {
                this.router.navigate(["home"]);
            },
            () => {
                Toast.makeText(
                    "Something went wrong. Try again",
                    "long"
                ).show();
            }
        );
    }
    public goToRegister() {
        this.router.navigate(["register"]);
    }
}
