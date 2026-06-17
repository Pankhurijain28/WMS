import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment }
from '../../../environments/environment';

@Injectable({
  providedIn:'root'
})
export class LeaveService {

  private apiUrl =
    `${environment.apiUrl}/Leave`;

  constructor(
    private http:HttpClient
  ){}

  getAll(){
    return this.http.get(this.apiUrl);
  }

  create(data:any){
    return this.http.post(
      this.apiUrl,
      data
    );
  }

  approve(id:number){
    return this.http.put(
      `${this.apiUrl}/${id}/approve`,
      {}
    );
  }

  reject(id:number){
    return this.http.put(
      `${this.apiUrl}/${id}/reject`,
      {}
    );
  }

  cancel(id:number){
    return this.http.put(
      `${this.apiUrl}/${id}/cancel`,
      {}
    );
  }
}