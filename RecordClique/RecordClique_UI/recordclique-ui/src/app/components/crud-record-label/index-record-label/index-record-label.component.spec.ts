import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IndexRecordLabelComponent } from './index-record-label.component';

describe('IndexRecordLabelComponent', () => {
  let component: IndexRecordLabelComponent;
  let fixture: ComponentFixture<IndexRecordLabelComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [IndexRecordLabelComponent]
    });
    fixture = TestBed.createComponent(IndexRecordLabelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
