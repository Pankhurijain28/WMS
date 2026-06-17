import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-navbar',
  standalone: true,
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss'
})
export class NavbarComponent {

  username = localStorage.getItem('username') || 'User';
  role = localStorage.getItem('role') || 'Member';

  get initial(): string {
    return (this.username || 'U').charAt(0).toUpperCase();
  }

  constructor(
    private router: Router,
    private location: Location
  ) {}

  goBack() {
    this.location.back();
  }

  logout() {
    localStorage.clear();
    this.router.navigate(['/']);
  }
}
