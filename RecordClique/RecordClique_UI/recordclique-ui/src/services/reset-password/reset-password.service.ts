import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResetPassword } from 'src/app/models/reset-password.model';
import { BaseService } from '../base/base.service';

@Injectable({
  providedIn: 'root'
})
export class ResetPasswordService extends BaseService {

  constructor(private http: HttpClient) {
    super();
  }

  sendResetPasswordLink(email: string) {
    return this.http.post<any>(`${this.apiUrl}/send-reset-email/${email}`, {});
  }

  resetPassword(resetPasswordObj: ResetPassword) {
    return this.http.post<any>(`${this.apiUrl}/reset-password`, resetPasswordObj);
  }
}
