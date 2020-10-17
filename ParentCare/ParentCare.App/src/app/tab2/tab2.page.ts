import { Component } from '@angular/core';
import { DeviceMotion, DeviceMotionAccelerationData } from '@ionic-native/device-motion/ngx';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-tab2',
  templateUrl: 'tab2.page.html',
  styleUrls: ['tab2.page.scss']
})
export class Tab2Page {
  constructor(private deviceMotion: DeviceMotion) { }

  accelSubscription: Subscription;
  readonly g = 9.80665;


  // http://ww2.cs.fsu.edu/~sposaro/publications/iFall.pdf
  getCurrentAccel(){
    // Get the device current acceleration
    this.deviceMotion.getCurrentAcceleration().then(
      (acceleration: DeviceMotionAccelerationData) => {  
        console.log(acceleration);
        var gforce = Math.sqrt(acceleration.x * acceleration.x + acceleration.y * acceleration.y + acceleration.z * acceleration.z) / this.g;
        //http://ww2.cs.fsu.edu/~sposaro/publications/iFall.pdf
        // If the amplitude crosses the lower and upper thresholds in the set duration period a fall is suspected

        // the assumption is a fall can only start from an upright position and end in a horizontal position
        //[14]. Thus the difference in position before and after the fall is close to 90 ◦
        // [26]. A fall is only suspected if both thresholds are crossed within a duration and the position is changed.
      },
      (error: any) => console.log(error)
    );
  }

  watchAccel(){
    // Watch device acceleration
    this.accelSubscription = this.deviceMotion
      .watchAcceleration()
      .subscribe((acceleration: DeviceMotionAccelerationData) => {
        console.log(acceleration);
      });
  }

  stopWatchAccel(){
    // Stop watch
    this.accelSubscription.unsubscribe();
  }
}
