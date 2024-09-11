import { TestBed } from '@angular/core/testing';

import { RecordLabelsService } from './record-labels.service';

describe('RecordLabelsService', () => {
  let service: RecordLabelsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RecordLabelsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
