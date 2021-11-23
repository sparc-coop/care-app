import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateMedication } from './create-medication.page';

const routes: Routes = [
  {
    path: '',
    component: CreateMedication,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CreateMedicationRoutingModule {}
