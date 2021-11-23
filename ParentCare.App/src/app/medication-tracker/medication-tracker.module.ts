import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MedicationTracker } from './medication-tracker.page';
import { ExploreContainerComponentModule } from '../explore-container/explore-container.module';

import { MedicationTrackerRoutingModule } from './medication-tracker-routing.module'

@NgModule({
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    ExploreContainerComponentModule,
    RouterModule.forChild([{ path: '', component: MedicationTracker }]),
    MedicationTrackerRoutingModule,
  ],
  declarations: [MedicationTracker]
})
export class MedicationTrackerModule {}
