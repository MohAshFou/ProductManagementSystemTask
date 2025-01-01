import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllProducstComponent } from './all-producst.component';

describe('AllProducstComponent', () => {
  let component: AllProducstComponent;
  let fixture: ComponentFixture<AllProducstComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AllProducstComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AllProducstComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
