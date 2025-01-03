import { Injectable } from '@angular/core';
import { BaseService } from '../base/base.service';
import { PaginatedResponse } from 'src/app/models/paginated-response.model';
import { Album } from 'src/app/models/album.model';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AlbumsService extends BaseService {

  constructor(private http: HttpClient) {
    super();
   }

  getAlbums(pageNumber: number, pageSize: number, filterName?: string, artistId?: string, genreId?: string, year?: number, userId?: string): Observable<PaginatedResponse<Album>> {
    let queryParams: any = {};

    queryParams.pageNumber = pageNumber;
    queryParams.pageSize = pageSize;
   
    if (filterName) {
      queryParams.filterName = filterName;
    }

    if (artistId) {
      queryParams.artistId = artistId;
    }

    if (genreId) {
      queryParams.genreId = genreId;
    }

    if (year) {
      queryParams.year = year;
    }

    if (userId) {
      queryParams.userId = userId;
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

  getUserAllAlbums(pageNumber: number, pageSize: number, userId: string, type?: number): Observable<PaginatedResponse<Album>> {
    let queryParams = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString())
      .set('userId', userId);

    if (type !== undefined) {
      queryParams = queryParams.set('type', type.toString());
    }

    return this.http.get<PaginatedResponse<Album>>(this.apiUrl + '/Album/userAllAlbums', { params: queryParams });
  }

  updateUserAlbumLink(albumId: string, userId: string, ind: boolean, type: number): Observable<void> {
    let queryParams = new HttpParams()
      .set('albumId', albumId)
      .set('ind', ind.toString())
      .set('type', type.toString())
      .set('userId', userId);

    return this.http.put<void>(`${this.apiUrl}/Album/album-link`, {}, { params: queryParams });
  }

}
