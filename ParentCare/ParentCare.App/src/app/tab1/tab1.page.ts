import { Component, OnInit } from '@angular/core';
import { CallNumber } from '@ionic-native/call-number/ngx';
import { NativeStorage } from '@ionic-native/native-storage/ngx';
import { Caretaker } from 'src/model/Caretaker';


@Component({
  selector: 'app-tab1',
  templateUrl: 'tab1.page.html',
  styleUrls: ['tab1.page.scss']
})
export class Tab1Page implements OnInit {

  
  constructor(private callNumber: CallNumber,
    private nativeStorage: NativeStorage) { }

  caretaker: Caretaker = new Caretaker();
  validationMessages: string[] = [];

  ngOnInit() {
		this.loadCaretaker();
  }
  
  loadCaretaker(){
    this.nativeStorage.getItem('caretaker')
    .then(
      data => {
        if (data){
        this.caretaker = data;
      }},
      error => console.error(error)
    );
  }

  call(){
    if(!this.caretaker.Phone){
      // TODO: ask user to fill in the caretaker data
      return;
    }

    this.callNumber.callNumber(this.caretaker.Phone, true)
    .then(res => console.log('Launched dialer!', res))
    .catch(err => console.log('Error launching dialer', err));
  }

  saveCaretaker(){
    this.nativeStorage.setItem('caretaker', this.caretaker)
    .then(
      () => console.log('Stored caretaker!'),
      error => console.error('Error storing caretaker', error)
    );

    

    // TODO: save to API 

  }

  validate(caretaker: Caretaker){
    var valid = true;
    this.validationMessages = [];

    if (!caretaker.Name){
      valid = false;
      this.validationMessages.push("Please enter the caretaker name.")
    }
    if (!caretaker.Phone){
      valid = false;
      this.validationMessages.push("Please enter the caretaker phone.")
    }

    // if (!caretaker.Email){
    //   valid = false;
    //   this.validationMessages.push("Please enter the caretaker email address.")
    // }

    return valid;
  }
}
