import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss'
})
export class NavbarComponent {

  constructor(
    private router: Router
  ) {}

  logout() {

    localStorage.clear();

    this.router.navigate(['/']);
  }
}