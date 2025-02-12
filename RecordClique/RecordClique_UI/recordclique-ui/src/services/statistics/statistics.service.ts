import { Injectable } from '@angular/core';
import { BaseService } from '../base/base.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StatisticsService extends BaseService {

  constructor(private http: HttpClient) {
    super();
   }

   generateStatisticsReport() {
    return this.http.get(`${this.apiUrl}/Statistic/generate-report`,{observe: 'response', responseType: 'blob'});
  }

}
