import { Injectable } from '@angular/core';
import { BaseService } from '../base/base.service';
import { HttpClient } from '@angular/common/http';
import { PaginatedResponse } from 'src/app/models/paginated-response.model';
import { Artist } from 'src/app/models/artist.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ArtistsService extends BaseService {
  constructor(private http: HttpClient) {
    super();
   }

   addArtist(addArtistRequest: Artist): Observable<Artist>{
    addArtistRequest.Id = '00000000-0000-0000-0000-000000000000';
    return this.http.post<Artist>(this.apiUrl + '/Artist',addArtistRequest);
   }

   getArtists(pageNumber: number, pageSize: number, filterName?: string): Observable<PaginatedResponse<Artist>> {
    let queryParams: any = {};

    queryParams.pageNumber = pageNumber;
    queryParams.pageSize = pageSize;
   
    if (filterName) {
      queryParams.filterName = filterName;
    }
    return this.http.get<PaginatedResponse<Artist>>(this.apiUrl + '/artists', { params: queryParams });
  }

  updateArtist(updateArtistRequest: Artist): Observable<Artist>{
    return this.http.put<Artist>(this.apiUrl + '/Artist/', updateArtistRequest);
   }

  deleteArtist(id: string) : Observable<string>{
    return this.http.delete<string>(this.apiUrl + '/Artist/'+id);
   }

   getArtistById(id: string):Observable<Artist> {
    return this.http.get<Artist>(`${this.apiUrl}/Artist/`+ id);
  }  
}
