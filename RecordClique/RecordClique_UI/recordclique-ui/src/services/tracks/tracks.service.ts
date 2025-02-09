import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Track } from 'src/app/models/track.model';
import { BaseService } from '../base/base.service';

@Injectable({
  providedIn: 'root'
})
export class TracksService extends BaseService {

  constructor(private http: HttpClient) {
    super();
   }

public getTracks(albumId: string) {
  let queryParams: any = {};
  queryParams.albumId = albumId;

  return this.http.get<Track[]>(this.apiUrl + '/Track', { params: queryParams});
}

}
