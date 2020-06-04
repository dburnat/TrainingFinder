import { environment } from "~/environments/environment";
import { GymComponent } from "./components/gym/gym";
import { ErrorInterceptor } from "./helpers/error.interceptor";
import { JwtInterceptor } from "./helpers/jwt.interceptor";
import { GymCreateComponent } from "./components/gymcreate/gymcreate";
import { LoginComponent } from "./components/login/login";
import { AuthGuard } from "./helpers/authguard";
import { NativeScriptRouterModule } from "nativescript-angular/router";
import { NgModule, NO_ERRORS_SCHEMA, PlatformRef } from "@angular/core";
import { NativeScriptModule } from "nativescript-angular/nativescript.module";
import { NativeScriptHttpClientModule } from "nativescript-angular/http-client";
import { NativeScriptFormsModule } from "nativescript-angular/forms";
import { NativeScriptUISideDrawerModule } from "nativescript-ui-sidedrawer/angular";
import * as platform from "tns-core-modules/platform";
declare var GMSServices: any;

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { RegisterComponent } from "./components/register/register";
import { HomeComponent } from "./components/home/home";
import { GymsComponent } from "./components/gyms/gyms";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { TrainingcreateComponent } from "./components/trainingcreate/trainingcreate.component";
import { UsersTrainingsComponent } from "./components/userstrainings/userstrainings.component";

import { NativeScriptMaterialButtonModule } from "nativescript-material-button/angular";
import { NativeScriptMaterialCardViewModule } from "nativescript-material-cardview/angular";
import { NativeScriptMaterialTextFieldModule } from "nativescript-material-textfield/angular";
import { ModalDatetimepicker } from "nativescript-modal-datetimepicker"
import { ReactiveFormsModule } from "@angular/forms";

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
        ReactiveFormsModule
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
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
        ModalDatetimepicker
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
