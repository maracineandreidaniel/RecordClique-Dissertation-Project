import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateRecordLabelComponent } from './update-record-label.component';

describe('UpdateRecordLabelComponent', () => {
  let component: UpdateRecordLabelComponent;
  let fixture: ComponentFixture<UpdateRecordLabelComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UpdateRecordLabelComponent]
    });
    fixture = TestBed.createComponent(UpdateRecordLabelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
