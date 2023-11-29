import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BudgetViewComponent } from './budget-view.component';

describe('BudgetViewComponent', () => {
  let component: BudgetViewComponent;
  let fixture: ComponentFixture<BudgetViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BudgetViewComponent]
    });
    fixture = TestBed.createComponent(BudgetViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
