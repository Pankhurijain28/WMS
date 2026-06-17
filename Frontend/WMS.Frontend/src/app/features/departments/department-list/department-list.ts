import {
  Component,
  OnInit
} from '@angular/core';

import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

import { DepartmentService }
from '../../../core/services/department.service';

import { NavbarComponent }
from '../../../shared/layouts/navbar/navbar';

import { SidebarComponent }
from '../../../shared/layouts/sidebar/sidebar';

@Component({
  selector: 'app-department-list',
  standalone: true,
  imports: [CommonModule, NavbarComponent, SidebarComponent],
  templateUrl: './department-list.html',
  styleUrls: ['./department-list.css']
})
export class DepartmentListComponent
implements OnInit {

  departments: any[] = [];

  constructor(
    private service:
      DepartmentService,
    private router:
      Router
  ) {}

  ngOnInit(): void {

  console.log('DEPARTMENT COMPONENT LOADED');

  this.loadData();

}

loadData() {

  console.log('DEPARTMENT API CALL');

  this.service.getAll().subscribe({

    next: (res: any) => {

      console.log('DEPARTMENT DATA');
      console.log(res);

      this.departments = res;

      console.log('DEPARTMENT COUNT');
      console.log(this.departments.length);

    },

    error: (err) => {

      console.log('DEPARTMENT ERROR');
      console.log(err);

    }

  });
}

  loadDepartments() {

    this.service
      .getAll()
      .subscribe((res: any) => {

        this.departments = res;

      });
  }

  addDepartment() {

    this.router.navigate(
      ['/department-form']
    );
  }

  editDepartment(id: number) {

    this.router.navigate(
      ['/department-form', id]
    );
  }

  deleteDepartment(id: number) {

    if (!confirm(
      'Delete Department?'))
      return;

    this.service
      .delete(id)
      .subscribe(() => {

        this.loadDepartments();

      });
  }
}