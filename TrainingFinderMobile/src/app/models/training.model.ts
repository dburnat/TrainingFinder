import { Adapter } from "../adapter";
import { Injectable } from "@angular/core";

export class Training {
    constructor(
        public trainingId: number,
        public description: string,
        public dateTime: Date,
        public gymId: number
    ) {}
}
@Injectable({
    providedIn: "root",
})
export class TrainingAdapter implements Adapter<Training> {
    adapt(item: any): Training {
        return new Training(
            item.trainingId,
            item.description,
            new Date(),
            item.gymId
        );
    }
}
