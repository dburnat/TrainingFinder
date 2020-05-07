import { ErrorInterceptor } from "./helpers/error.interceptor";
import { JwtInterceptor } from "./helpers/jwt.interceptor";
import { GymCreateComponent } from "./components/gymcreate/gymcreate";
import { LoginComponent } from "./components/login/login";
import { AuthGuard } from "./helpers/authguard";
import { NativeScriptRouterModule } from "nativescript-angular/router";
import { NgModule, NO_ERRORS_SCHEMA } from "@angular/core";
import { NativeScriptModule } from "nativescript-angular/nativescript.module";
import { NativeScriptHttpClientModule } from "nativescript-angular/http-client";
import { NativeScriptFormsModule } from "nativescript-angular/forms";
import { NativeScriptUISideDrawerModule } from "nativescript-ui-sidedrawer/angular";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { RegisterComponent } from "./components/register/register";
import { HomeComponent } from "./components/home/home";
import { GymsComponent } from "./components/gyms/gyms";
import { HTTP_INTERCEPTORS } from "@angular/common/http";

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
    ],
    declarations: [
        AppComponent,
        RegisterComponent,
        HomeComponent,
        LoginComponent,
        GymsComponent,
        GymCreateComponent,
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    ],
    schemas: [NO_ERRORS_SCHEMA],
})
/*
Pass your application module to the bootstrapModule function located in main.ts to start your app
*/
export class AppModule {}
