import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AllocationService {

  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get(`${environment.apiUrl}/Allocations`);
  }

  getById(id: number) {
    return this.http.get(`${environment.apiUrl}/Allocations/${id}`);
  }

  create(data: any) {
    return this.http.post(`${environment.apiUrl}/Allocations`, data);
  }

  update(id: number, data: any) {
    return this.http.put(`${environment.apiUrl}/Allocations/${id}`, data);
  }

  delete(id: number) {
    return this.http.delete(`${environment.apiUrl}/Allocations/${id}`);
  }
}