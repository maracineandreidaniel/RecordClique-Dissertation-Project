import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RecordLabel } from 'src/app/models/record-label.model';
import { BaseService } from '../base/base.service';
import { HttpClient } from '@angular/common/http';
import { PaginatedResponse } from 'src/app/models/paginated-response.model';

@Injectable({
  providedIn: 'root'
})
export class RecordLabelsService extends BaseService {

  constructor(private http: HttpClient) {
    super();
   }

  getRecordLabelById(id: string):Observable<RecordLabel> {
    return this.http.get<RecordLabel>(`${this.apiUrl}/RecordLabel/`+ id);
  }

  getRecordLabels(pageNumber: number, pageSize: number): Observable<PaginatedResponse<RecordLabel>> {
    let queryParams: any = {};

    queryParams.pageNumber = pageNumber;
    queryParams.pageSize = pageSize;
   
    return this.http.get<PaginatedResponse<RecordLabel>>(this.apiUrl + '/record-labels', { params: queryParams });
  }

  deleteRecordLabel(id: string) : Observable<string>{
    return this.http.delete<string>(this.apiUrl + '/RecordLabel/'+id);
   }

}
