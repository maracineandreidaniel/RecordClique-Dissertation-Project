import { Injectable } from '@angular/core';
import { BaseService } from '../base/base.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SelectOptionResult } from 'src/app/models/select-option-result.model';

@Injectable({
  providedIn: 'root'
})
export class GenresService extends BaseService {

  constructor(private http: HttpClient) {
    super();
   }

   getGenreSelectOptions():Observable<SelectOptionResult[]> {
    return this.http.get<SelectOptionResult[]>(`${this.apiUrl}/Genre/SelectOptions`);
  }  
  
}
