import { Injectable } from '@angular/core';
import { Gym } from '../models/gym.model';
@Injectable({
    providedIn: 'root',
  })
export class AppDataService {

    myGym: Gym;
    constructor() {

    }
    saveGym(gym : Gym){
        this.myGym = gym;
    }
    retrieveGym(){
        return this.myGym;
    }

}
