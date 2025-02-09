import { Injectable } from '@angular/core';
import { BaseService } from '../base/base.service';
import { Observable } from 'rxjs';
import { PaginatedResponse } from 'src/app/models/paginated-response.model';
import { Review } from 'src/app/models/review.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ReviewsService extends BaseService {

  constructor(private http: HttpClient) {
    super();
   }

   getReviews(pageNumber: number, pageSize: number, albumId: string): Observable<PaginatedResponse<Review>> {
       let queryParams: any = {};
   
       queryParams.pageNumber = pageNumber;
       queryParams.pageSize = pageSize;
       queryParams.albumId = albumId;
      
       return this.http.get<PaginatedResponse<Review>>(this.apiUrl + '/Review', { params: queryParams });
     }

     deleteReview(id: string) : Observable<string>{
      let queryParams: any = {};
      queryParams.reviewId = id;
      return this.http.delete<string>(this.apiUrl + '/Review', { params: queryParams });
     }

     addReview(addReviewRequest: Review): Observable<Review>{
        //addReviewRequest.Id = '00000000-0000-0000-0000-000000000000';
         return this.http.post<Review>(this.apiUrl + '/Review',addReviewRequest);
        }
}
