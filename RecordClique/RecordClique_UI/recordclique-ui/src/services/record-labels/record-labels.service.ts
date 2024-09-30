import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RecordLabel } from 'src/app/models/record-label.model';
import { BaseService } from '../base/base.service';
import { HttpClient } from '@angular/common/http';
import { PaginatedResponse } from 'src/app/models/paginated-response.model';
import { SelectOptionResult } from 'src/app/models/select-option-result.model';

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

  addRecordLabel(addRecordLabelRequest: RecordLabel): Observable<RecordLabel>{
    addRecordLabelRequest.Id = '00000000-0000-0000-0000-000000000000';
    return this.http.post<RecordLabel>(this.apiUrl + '/RecordLabel',addRecordLabelRequest);
  }

  updateRecordLabel(updateRecordLabelRequest: RecordLabel): Observable<RecordLabel>{
  return this.http.put<RecordLabel>(this.apiUrl + '/RecordLabel/', updateRecordLabelRequest);
  }

  getRecordLabelSelectOptions():Observable<SelectOptionResult[]> {
    return this.http.get<SelectOptionResult[]>(`${this.apiUrl}/RecordLabel/SelectOptions`);
  }  

}
