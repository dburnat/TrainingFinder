import { AuthenticationService } from "./services/authentication.service";
import { Component, OnInit } from "@angular/core";
import { NavigationEnd, Router } from "@angular/router";
import {
    DrawerTransitionBase,
    RadSideDrawer,
    SlideInOnTopTransition,
} from "nativescript-ui-sidedrawer";
import { filter } from "rxjs/operators";
import { RouterExtensions } from "@nativescript/angular";
import { Application } from "@nativescript/core";
const rootView = Application.getRootView();

@Component({
    selector: "ns-app",
    templateUrl: "app.component.html",
})
export class AppComponent implements OnInit {
    private _activatedUrl: string;
    private _sideDrawerTransition: DrawerTransitionBase;
    public drawer: RadSideDrawer;
    public userName: string;

    constructor(
        private router: Router,
        private routerExtensions: RouterExtensions,
        public authenticationService: AuthenticationService
    ) {
        // Use the component constructor to inject services.
    }

    ngOnInit(): void {
        this._sideDrawerTransition = new SlideInOnTopTransition();

        this.router.events
            .pipe(filter((event: any) => event instanceof NavigationEnd))
            .subscribe(
                (event: NavigationEnd) =>
                    (this._activatedUrl = event.urlAfterRedirects)
            );
    }

    get sideDrawerTransition(): DrawerTransitionBase {
        return this._sideDrawerTransition;
    }

    isComponentSelected(url: string): boolean {
        return this._activatedUrl === url;
    }

    onNavItemTap(navItemRoute: string): void {
        this.routerExtensions.navigate([navItemRoute], {
            transition: {
                name: "fade",
            },
        });

        const sideDrawer = <RadSideDrawer>rootView;
        sideDrawer.closeDrawer();
    }

    logoutTap() {
        this.drawer = <RadSideDrawer>rootView;
        this.drawer.closeDrawer();
        setTimeout(async () => {
            await this.authenticationService.logout();
            this.routerExtensions.navigateByUrl("login", {
                clearHistory: true,
            });
        }, 500);
    }
}
