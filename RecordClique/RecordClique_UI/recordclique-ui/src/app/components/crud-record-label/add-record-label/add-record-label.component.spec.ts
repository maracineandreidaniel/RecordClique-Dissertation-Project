import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddRecordLabelComponent } from './add-record-label.component';

describe('AddRecordLabelComponent', () => {
  let component: AddRecordLabelComponent;
  let fixture: ComponentFixture<AddRecordLabelComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddRecordLabelComponent]
    });
    fixture = TestBed.createComponent(AddRecordLabelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
