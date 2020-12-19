import { ProfileComponent } from "./components/profile/profile.component";
import { ModalDatetimepicker } from "nativescript-modal-datetimepicker";
import { environment } from "~/environments/environment";
import { GymComponent } from "./components/gym/gym.component";
import { ErrorInterceptor } from "./helpers/error.interceptor";
import { JwtInterceptor } from "./helpers/jwt.interceptor";
import { GymCreateComponent } from "./components/gymcreate/gymcreate.component";
import { LoginComponent } from "./components/login/login.component";
import { AuthGuard } from "./helpers/authguard";
import { NgModule, NO_ERRORS_SCHEMA } from "@angular/core";
import { NativeScriptUISideDrawerModule } from "nativescript-ui-sidedrawer/angular";
declare var GMSServices: any;

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { RegisterComponent } from "./components/register/register.component";
import { HomeComponent } from "./components/home/home.component";
import { GymsComponent } from "./components/gyms/gyms.component";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { TrainingcreateComponent } from "./components/trainingcreate/trainingcreate.component";
import { UsersTrainingsComponent } from "./components/userstrainings/userstrainings.component";

import { ReactiveFormsModule } from "@angular/forms";
import {
    NativeScriptModule,
    NativeScriptFormsModule,
    NativeScriptHttpClientModule,
    NativeScriptRouterModule,
} from "@nativescript/angular";
import { NativeScriptMaterialButtonModule } from "@nativescript-community/ui-material-button/angular";
import { NativeScriptMaterialCardViewModule } from "@nativescript-community/ui-material-cardview/angular";
import { NativeScriptMaterialTextFieldModule } from "@nativescript-community/ui-material-textfield/angular";
import * as platform from "platform";

let routes = [
    { path: "", component: RegisterComponent },
    { path: "home", component: HomeComponent, canActivate: [AuthGuard] },
];

@NgModule({
    bootstrap: [AppComponent],
    imports: [
        NativeScriptModule,
        AppRoutingModule,
        NativeScriptFormsModule,
        NativeScriptHttpClientModule,
        NativeScriptRouterModule,
        NativeScriptRouterModule.forRoot(routes),
        NativeScriptUISideDrawerModule,
        NativeScriptMaterialButtonModule,
        NativeScriptMaterialCardViewModule,
        NativeScriptMaterialTextFieldModule,
        ReactiveFormsModule,
    ],
    declarations: [
        AppComponent,
        RegisterComponent,
        HomeComponent,
        LoginComponent,
        GymsComponent,
        GymCreateComponent,
        GymComponent,
        TrainingcreateComponent,
        UsersTrainingsComponent,
        ProfileComponent,
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
        ModalDatetimepicker,
    ],
    schemas: [NO_ERRORS_SCHEMA],
})
/*
Pass your application module to the bootstrapModule function located in main.ts to start your app
*/
export class AppModule {}

if (platform.isIOS) {
    GMSServices.provideAPIKey(`${environment.googleApiKey}`);
}
