import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CreateMedication } from './create-medication.page';
import { ExploreContainerComponentModule } from '../../explore-container/explore-container.module';

import { CreateMedicationRoutingModule } from './create-medication-routing.module';

@NgModule({
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    ExploreContainerComponentModule,
    CreateMedicationRoutingModule
  ],
  declarations: [CreateMedication]
})
export class CreateMedicationModule {}
