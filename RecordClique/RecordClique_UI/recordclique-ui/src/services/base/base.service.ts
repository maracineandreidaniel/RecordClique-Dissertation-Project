import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export abstract class BaseService {
  protected apiUrl: string = 'https://localhost:7125'; //Development
  // protected apiUrl: string = 'http://192.168.168.198:83'; //IIS 

  constructor() { }
}
