import {
  Component,
  OnInit
} from '@angular/core';

import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

import { RoleService }
from '../../../core/services/role.service';

import { NavbarComponent }
from '../../../shared/layouts/navbar/navbar';

import { SidebarComponent }
from '../../../shared/layouts/sidebar/sidebar';

@Component({
  selector: 'app-role-list',
  standalone: true,
  imports: [CommonModule, NavbarComponent, SidebarComponent],
  templateUrl: './role-list.html',
  styleUrl: './role-list.css'
})
export class RoleListComponent
implements OnInit {

  roles: any[] = [];

  constructor(
    private service: RoleService,
    private router: Router
  ) {}

  ngOnInit(): void {

    this.loadRoles();

  }

  loadRoles() {

    this.service
      .getAll()
      .subscribe((res: any) => {

        this.roles = res;

      });
  }

  addRole() {

    this.router.navigate(
      ['/role-form']
    );
  }

  editRole(id: number) {

    this.router.navigate(
      ['/role-form', id]
    );
  }

  deleteRole(id: number) {

    if (!confirm(
      'Delete Role?'))
      return;

    this.service
      .delete(id)
      .subscribe(() => {

        this.loadRoles();

      });
  }
}