import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from 'src/app/models/user.model';
import { BaseService } from '../base/base.service';

@Injectable({
  providedIn: 'root'
})
export class UserStoreService extends BaseService {
  private fullName$ = new BehaviorSubject<string>("");
  private role$ = new BehaviorSubject<string>("");
  private id$ = new BehaviorSubject<string>("");

  constructor(private http: HttpClient) {
    super();
  }

  public getRoleFromStore(){
    return this.role$.asObservable();
  }

  public setRoleForStore(role: string){
    this.role$.next(role);
  }

  public getFullNameFromStore(){
    return this.fullName$.asObservable();
  }

  public setFullNameForStore(fullName: string){
    this.fullName$.next(fullName);
  }

  public getIdFromStore(){
    return this.id$.asObservable();
  }

  public setIdForStore(id: string){
    this.id$.next(id);
  }

  public getUserDetails(username: string): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/user-details?username=${username}`);
  }

  public getUserInitials(username: string): Observable<string> {
    return this.http.get(`${this.apiUrl}/user-initials?username=${username}`, { responseType: 'text' });
  }
}
