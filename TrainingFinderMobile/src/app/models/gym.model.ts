import { Adapter } from "../adapter";
import { Injectable } from "@angular/core";

export class Gym {
    constructor(
        public gymId: number,
        public name: string,
        public city: string,
        public street: string,
        public number: string,
        public postCode: string,
        public latitude: number,
        public longitude: number,
        public trainings: []
    ) {}
}
@Injectable({
    providedIn: "root",
})
export class GymAdapter implements Adapter<Gym> {
    adapt(item: any): Gym {
        return new Gym(
            item.gymId,
            item.name,
            item.city,
            item.street,
            item.number,
            item.postCode,
            item.latitude,
            item.longitude,
            item.trainings
        );
    }
}
