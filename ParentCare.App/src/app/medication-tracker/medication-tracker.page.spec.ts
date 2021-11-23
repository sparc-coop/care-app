import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';
import { ExploreContainerComponentModule } from '../explore-container/explore-container.module';

import { MedicationTracker } from './medication-tracker.page';

describe('MedicationTracker', () => {
  let component: MedicationTracker;
  let fixture: ComponentFixture<MedicationTracker>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [MedicationTracker],
      imports: [IonicModule.forRoot(), ExploreContainerComponentModule]
    }).compileComponents();

    fixture = TestBed.createComponent(MedicationTracker);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
