import { Time } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Platform } from '@ionic/angular';
import { LocalNotifications } from '@ionic-native/local-notifications/ngx';
import { NativeStorage } from '@ionic-native/native-storage/ngx';

@Component({
  selector: 'app-tab2',
  templateUrl: 'tab2.page.html',
  styleUrls: ['tab2.page.scss']
})
export class Tab2Page implements OnInit{

  constructor(private localNotifications: LocalNotifications, 
    public platform: Platform, 
    private nativeStorage: NativeStorage) {
    // this.newAlarm = new MedicationAlarm();
  }

  newAlarm: MedicationAlarm = new MedicationAlarm();
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
    this.alarms.push(new MedicationAlarm({Time: new Date(), Title: "Cylocort" }));
    this.alarms.push(new MedicationAlarm({Time: new Date(), Title: "Neosoro" }));
    return;

    this.nativeStorage.getItem('alarms')
    .then(
      data => this.alarms = data,
      error => console.error(error)
    );
  }

  saveAlarm() {
    if (!this.validate(this.newAlarm)){
      return;
    }

    var selectedWeekdays = this.weekdays.reduce((a, o) => (o.isChecked && a.push(o.val), a), []);
    console.log(selectedWeekdays);      
    console.log("SAVE ALARM");
    this.newAlarm.Weekdays = selectedWeekdays;
    this.newAlarm.Time = new Date(this.newAlarm.Time);
    this.createNotification(this.newAlarm);
    this.alarms.push(this.newAlarm);
    this.newAlarm = new MedicationAlarm();

    this.nativeStorage.setItem('alarms', this.alarms)
    .then(
      () => console.log('Stored item!'),
      error => console.error('Error storing item', error)
    );

    
  }

  createNotification(alarm: MedicationAlarm){
    // this.localNotifications.requestPermission(function (granted) { console.log(""); });
    // https://github.com/katzer/cordova-plugin-local-notifications

    console.log(alarm.Time);

    if (!alarm.Weekdays){
      // TODO: run only once if no weekdays are selected
      this.localNotifications.schedule({
        title: alarm.Title,
        sound: this.platform.is("android") ? 'file://sound.mp3': 'file://beep.caf',
        trigger: { every: { hour: alarm.Time.getHours(), minute: alarm.Time.getMinutes() }}
      });
    }
    else{
      for (var i = 0; i < alarm.Weekdays.length; i++) {
        this.localNotifications.schedule({
          title: alarm.Title,
          sound: this.platform.is("android") ? 'file://sound.mp3': 'file://beep.caf',
          trigger: { every: { weekday: alarm.Weekdays[i], hour: alarm.Time.getHours(), minute: alarm.Time.getMinutes() } }
        });
      }
    }
  }

  validate(alarm: MedicationAlarm){
    var valid = true;
    this.validationMessages = [];

    if (!alarm.Time){
      valid = false;
      this.validationMessages.push("Please set a time.")
    }
    if (!alarm.Title){
      valid = false;
      this.validationMessages.push("Please enter the medication name.")
    }

    return valid;
  }

}

export class MedicationAlarm {
  Title: string;
  Time: Date;
  Weekdays: number[];

  public constructor(init?:Partial<MedicationAlarm>) {
    Object.assign(this, init);
}
}
