import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment }
from '../../../environments/environment';

@Injectable({
  providedIn:'root'
})
export class AttendanceService {

  private apiUrl =
    `${environment.apiUrl}/Attendance`;

  constructor(
    private http:HttpClient
  ){}

  getAll(){
    return this.http.get(this.apiUrl);
  }

  checkIn(){
    return this.http.post(
      `${this.apiUrl}/checkin`,
      {}
    );
  }

  checkOut(){
    return this.http.post(
      `${this.apiUrl}/checkout`,
      {}
    );
  }
}