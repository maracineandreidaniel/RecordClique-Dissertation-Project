import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-star-rating',
  templateUrl: './star-rating.component.html',
  styleUrls: ['./star-rating.component.css']
})
export class StarRatingComponent {
  stars: boolean[] = [false, false, false, false, false]; 
  @Output() ratingChange = new EventEmitter<number>();

  setRating(index: number) {
    this.stars = this.stars.map((_, i) => i <= index);
    this.ratingChange.emit(index + 1);
  }
}
