import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MedicationTracker } from './medication-tracker.page';

const routes: Routes = [
  {
    path: '',
    component: MedicationTracker,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MedicationTrackerRoutingModule {}
