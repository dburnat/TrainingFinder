import { Page } from "tns-core-modules/ui/page";

export function BasePage() {
  return (target: any) => {
    const ngOnInit = target.prototype.ngOnInit;
    const ngOnDestroy = target.prototype.ngOnDestroy;

    target.prototype.ngOnInit = function(...args) {
      if (this.injector) {
        const page: Page = this.injector.get(Page);

        page.once(Page.unloadedEvent, (event: any) => {
          ngOnDestroy && ngOnDestroy.apply(this);
        });
      } else {
        console.warn("Please provide Injector in the constructor");
      }

      ngOnInit && ngOnInit.apply(this);
    };
  };
}
