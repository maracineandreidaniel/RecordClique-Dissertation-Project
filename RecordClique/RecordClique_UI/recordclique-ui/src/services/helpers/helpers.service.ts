import { Injectable } from '@angular/core';
import { BaseService } from '../base/base.service';
import { Movie } from 'src/app/models/movie.model';

@Injectable({
  providedIn: 'root'
})
export class HelpersService {
  constructor() {
  }

  getMovieTypeDisplay(movie: Movie): string {
    switch (movie?.Type) {
        case 1 :
            return '2D';
        case 2:
            return '3D';
        default:
            return 'Unknown';
    }
  }
  
  formatMovieTimeDuration(movie: Movie): string{
    return (Math.floor(movie?.TimeDuration/60) + "h:" + movie?.TimeDuration%60 + " min" ) ;
  }
  
  formatHour(hour: string): string{
    const dateHourString = hour;
    const dateObject = new Date(dateHourString);
    const formattedTime = dateObject.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
    return formattedTime;
  }

  
}
