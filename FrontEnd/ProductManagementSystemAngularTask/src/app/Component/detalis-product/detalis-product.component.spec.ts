import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalisProductComponent } from './detalis-product.component';

describe('DetalisProductComponent', () => {
  let component: DetalisProductComponent;
  let fixture: ComponentFixture<DetalisProductComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DetalisProductComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetalisProductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
