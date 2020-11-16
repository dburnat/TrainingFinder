import { AuthGuard } from "./helpers/authguard";
import { NgModule } from "@angular/core";
import { Routes } from "@angular/router";
import { HomeComponent } from "./components/home/home.component";
import { LoginComponent } from "./components/login/login.component";
import { RegisterComponent } from "./components/register/register.component";
import { GymsComponent } from "./components/gyms/gyms.component";
import { GymCreateComponent } from "./components/gymcreate/gymcreate.component";
import { GymComponent } from "./components/gym/gym.component";
import { TrainingcreateComponent } from "./components/trainingcreate/trainingcreate.component";
import { UsersTrainingsComponent } from "./components/userstrainings/userstrainings.component";
import { NativeScriptRouterModule } from "@nativescript/angular";

const routes: Routes = [
    { path: "", component: HomeComponent, canActivate: [AuthGuard] },
    { path: "login", component: LoginComponent },
    { path: "register", component: RegisterComponent },
    { path: "gyms", component: GymsComponent, canActivate: [AuthGuard] },
    {
        path: "gymCreate",
        component: GymCreateComponent,
        canActivate: [AuthGuard],
    },
    { path: "gym", component: GymComponent, canActivate: [AuthGuard] },
    {
        path: "trainingCreate",
        component: TrainingcreateComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "usersTrainings",
        component: UsersTrainingsComponent,
        canActivate: [AuthGuard],
    },
];

@NgModule({
    imports: [NativeScriptRouterModule.forRoot(routes)],
    exports: [NativeScriptRouterModule],
})
export class AppRoutingModule {}
