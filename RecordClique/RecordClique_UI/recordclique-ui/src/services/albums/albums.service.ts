import { Injectable } from '@angular/core';
import { BaseService } from '../base/base.service';
import { PaginatedResponse } from 'src/app/models/paginated-response.model';
import { Album } from 'src/app/models/album.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AlbumsService extends BaseService {

  constructor(private http: HttpClient) {
    super();
   }

  getAlbums(pageNumber: number, pageSize: number, filterName?: string): Observable<PaginatedResponse<Album>> {
    let queryParams: any = {};

    queryParams.pageNumber = pageNumber;
    queryParams.pageSize = pageSize;
   
    if (filterName) {
      queryParams.filterName = filterName;
    }
    return this.http.get<PaginatedResponse<Album>>(this.apiUrl + '/albums', { params: queryParams });
  }

  deleteAlbum(id: string) : Observable<string>{
    return this.http.delete<string>(this.apiUrl + '/Album/'+id);
   }

   addAlbum(addAlbumRequest: Album): Observable<Album>{
    addAlbumRequest.Id = '00000000-0000-0000-0000-000000000000';
    return this.http.post<Album>(this.apiUrl + '/Album',addAlbumRequest);
   }

   updateAlbum(updateAlbumRequest: Album): Observable<Album>{
    return this.http.put<Album>(this.apiUrl + '/Album/', updateAlbumRequest);
   }

   getAlbumById(id: string):Observable<Album> {
    return this.http.get<Album>(`${this.apiUrl}/Album/`+ id);
  }  
}
