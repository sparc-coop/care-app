import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';
import { ExploreContainerComponentModule } from '../../explore-container/explore-container.module';

import { CreateMedication } from './create-medication.page';

describe('CreateMedication', () => {
  let component: CreateMedication;
  let fixture: ComponentFixture<CreateMedication>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [CreateMedication],
      imports: [IonicModule.forRoot(), ExploreContainerComponentModule]
    }).compileComponents();

    fixture = TestBed.createComponent(CreateMedication);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
