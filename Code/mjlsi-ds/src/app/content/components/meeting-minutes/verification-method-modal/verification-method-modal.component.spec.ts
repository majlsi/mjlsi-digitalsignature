import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VerificationMethodModalComponent } from './verification-method-modal.component';

describe('VerificationMethodModalComponent', () => {
  let component: VerificationMethodModalComponent;
  let fixture: ComponentFixture<VerificationMethodModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VerificationMethodModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VerificationMethodModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
