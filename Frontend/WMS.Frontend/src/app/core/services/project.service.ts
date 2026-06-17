import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(
    private http: HttpClient
  ) {}

  getAll() {
    return this.http.get(
      `${environment.apiUrl}/Projects`
    );
  }

  getById(id: number) {
    return this.http.get(
      `${environment.apiUrl}/Projects/${id}`
    );
  }

  create(data: any) {
    return this.http.post(
      `${environment.apiUrl}/Projects`,
      data
    );
  }

  update(id: number, data: any) {
    return this.http.put(
      `${environment.apiUrl}/Projects/${id}`,
      data
    );
  }

  delete(id: number) {
    return this.http.delete(
      `${environment.apiUrl}/Projects/${id}`
    );
  }
}