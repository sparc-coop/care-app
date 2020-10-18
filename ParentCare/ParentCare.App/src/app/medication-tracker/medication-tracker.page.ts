import { Time } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ModalController, Platform } from '@ionic/angular';
import { LocalNotifications } from '@ionic-native/local-notifications/ngx';
import { NativeStorage } from '@ionic-native/native-storage/ngx';
import { MedicationAlarm } from 'src/model/MedicationDefinition';
import { CreateMedication } from '../medication/create-medication/create-medication.page';


@Component({
  selector: 'app-medication-tracker',
  templateUrl: 'medication-tracker.page.html',
  styleUrls: ['medication-tracker.page.scss']
})
export class MedicationTracker implements OnInit{

  constructor(private localNotifications: LocalNotifications, 
    public platform: Platform, 
    private nativeStorage: NativeStorage,
    public modalController: ModalController) {
    
  }

  alarms: MedicationAlarm[] = [];
  validationMessages: string[] = [];

  public weekdays = [
    { val: 0, title: 'Sunday', isChecked: false },
    { val: 1, title: 'Monday', isChecked: false },
    { val: 2, title: 'Tuesday', isChecked: false },
    { val: 3, title: 'Wednesday', isChecked: false },
    { val: 4, title: 'Thursday', isChecked: false },
    { val: 5, title: 'Friday', isChecked: false },
    { val: 6, title: 'Saturday', isChecked: false },
  ];

  ngOnInit() {
		this.loadAlarms();
	}

  loadAlarms(){
    this.alarms.push(new MedicationAlarm({Date: new Date(new Date().getTime() - 60*60000), Title: "Cylocort" }));
    this.alarms.push(new MedicationAlarm({Date: new Date(new Date().getTime() - 120*60000), Title: "Neosoro" }));
    this.alarms.push(new MedicationAlarm({Date: new Date(new Date().getTime() - 180*60000), Title: "Hydrocodone" }));
    this.alarms.push(new MedicationAlarm({Date: new Date(new Date().getTime() - 240*60000), Title: "Levothyroxine" }));
    this.alarms.push(new MedicationAlarm({Date: new Date(new Date().getTime() - 360*60000), Title: "Amlodipine Besylate" }));
    return;

    this.nativeStorage.getItem('alarms')
    .then(
      data => this.alarms = data,
      error => console.error(error)
    );
  }

  getOrderedAlarms(){
    return this.alarms;
    // return this.sortBy(this.alarms, )
  }

  sortBy(arr: any[], prop: string) {
    return arr.sort((a, b) => a[prop] > b[prop] ? 1 : a[prop] === b[prop] ? 0 : -1);
  }

  async openModal(){
    const modal = await this.modalController.create({
      component: CreateMedication,
      cssClass: 'create-medication-root',
      swipeToClose: true,
    });
    return await modal.present();
  }
}