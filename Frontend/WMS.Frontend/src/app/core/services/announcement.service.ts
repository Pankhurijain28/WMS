import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment }
from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AnnouncementService {

  private apiUrl =
    `${environment.apiUrl}/Announcement`;

  constructor(
    private http: HttpClient
  ) {}

  getAll() {
    return this.http.get(this.apiUrl);
  }

  create(data: any) {
    return this.http.post(
      this.apiUrl,
      data
    );
  }

  delete(id: number) {
    return this.http.delete(
      `${this.apiUrl}/${id}`
    );
  }
}