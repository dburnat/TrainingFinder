import { GymCreateComponent } from "./components/gymcreate/gymcreate";
import { GymComponent } from "./components/gym/gym";
import { AuthGuard } from "./helpers/authguard";
import { LoginComponent } from "./components/login/login";
import { NgModule } from "@angular/core";
import { NativeScriptRouterModule } from "nativescript-angular/router";
import { Routes } from "@angular/router";

import { RegisterComponent } from "./components/register/register";
import { HomeComponent } from "./components/home/home";

const routes: Routes = [
    { path: "", component: HomeComponent, canActivate: [AuthGuard] },
    { path: "login", component: LoginComponent },
    { path: "register", component: RegisterComponent },
    { path: "gym", component: GymComponent, canActivate: [AuthGuard] },
    { path: "gymCreate", component: GymCreateComponent, canActivate: [AuthGuard] },
];

@NgModule({
    imports: [NativeScriptRouterModule.forRoot(routes)],
    exports: [NativeScriptRouterModule],
})
export class AppRoutingModule {}
