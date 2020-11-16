import { AuthenticationService } from "../../services/authentication.service";
import { Router } from "@angular/router";
import { Component } from "@angular/core";
import * as Toast from "nativescript-toast";
import { RadSideDrawer } from "nativescript-ui-sidedrawer";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { Application } from "@nativescript/core";
const rootView = Application.getRootView();
@Component({
    selector: "login",
    templateUrl: "login.component.html",
    styleUrls: ["login.component.css"],
})
export class LoginComponent {
    public drawer: RadSideDrawer;
    public loginForm: FormGroup;

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

    async ngOnInit(): Promise<void> {
        this.loginForm = this.formBuilder.group({
            username: ["", Validators.required],
            password: ["", Validators.required],
        });
        await this.delay(500);
    }

    delay(ms: number) {
        return new Promise((resolve) => setTimeout(resolve, ms));
    }

    ngAfterViewInit() {
        setTimeout(() => {
            this.drawer = <RadSideDrawer>rootView;
            this.drawer.gesturesEnabled = false;
        }, 100);
    }

    public login(userName: string, password: string) {
        if (userName === null || password === null) return;

        return this.authenticationService.login(userName, password).subscribe(
            (result) => {
                this.router.navigate(["home"]);
            },
            (error) => {
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
