import { NgModule } from "@angular/core";
import { NativeScriptRouterModule } from "nativescript-angular/router";
import { Routes } from "@angular/router";

import { ItemsComponent } from "./item/items.component";
import { ItemDetailComponent } from "./item/item-detail.component";
import { LoginComponent } from "./components/login/login";
import { HomeComponent } from "./components/home/home";
import { AuthGuard } from "./helpers/authguard";

const routes: Routes = [
    {path: "", component: LoginComponent},
    {path: "home", component: HomeComponent, canActivate: [AuthGuard]}
];

@NgModule({
    imports: [NativeScriptRouterModule.forRoot(routes)],
    exports: [NativeScriptRouterModule]
})
export class AppRoutingModule { }
