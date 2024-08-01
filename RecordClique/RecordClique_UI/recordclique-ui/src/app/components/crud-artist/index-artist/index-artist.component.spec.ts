import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IndexArtistComponent } from './index-artist.component';

describe('IndexArtistComponent', () => {
  let component: IndexArtistComponent;
  let fixture: ComponentFixture<IndexArtistComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [IndexArtistComponent]
    });
    fixture = TestBed.createComponent(IndexArtistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
