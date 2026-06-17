import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private http: HttpClient
  ) {}

  login(data: any) {
    return this.http.post(
      `${environment.apiUrl}/Auth/login`,
      data
    );
  }

  logout() {
    localStorage.clear();
  }

  getRole(): string {
    return localStorage.getItem('role') || '';
  }

  isAdmin(): boolean {
    return this.getRole() === 'Admin';
  }

  isManager(): boolean {
    return this.getRole() === 'Manager';
  }

  isEmployee(): boolean {
    return this.getRole() === 'Employee';
  }

  /** Admin or Manager — i.e. can perform management actions */
  canManage(): boolean {
    return this.isAdmin() || this.isManager();
  }
}