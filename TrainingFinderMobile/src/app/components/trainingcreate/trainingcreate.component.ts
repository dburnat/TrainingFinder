import { Component, OnInit } from '@angular/core';
import { RadSideDrawer } from 'nativescript-ui-sidedrawer';
import * as app from "tns-core-modules/application";

@Component({
  selector: 'ns-trainingcreate',
  templateUrl: './trainingcreate.component.html',
  styleUrls: ['./trainingcreate.component.css']
})
export class TrainingcreateComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  onDrawerButtonTap(): void {
    const sideDrawer = <RadSideDrawer>app.getRootView();
    sideDrawer.showDrawer();
}
}
