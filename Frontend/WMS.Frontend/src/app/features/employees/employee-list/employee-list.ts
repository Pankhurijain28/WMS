import {

  Component,

  OnInit

} from '@angular/core';

import { FormsModule } from '@angular/forms';

import { CommonModule } from '@angular/common';

import { Router } from '@angular/router';



import { EmployeeService }

from '../../../core/services/employee.service';

import { SidebarComponent } from '../../../shared/layouts/sidebar/sidebar';

import { NavbarComponent } from '../../../shared/layouts/navbar/navbar';



@Component({

  selector: 'app-employee-list',

  standalone: true,

  imports: [CommonModule,

  NavbarComponent,

  SidebarComponent,

  FormsModule],

  templateUrl: './employee-list.html',

  styleUrl: './employee-list.css'

})

export class EmployeeListComponent

  implements OnInit {



  employees: any[] = [];
  searchName = '';

departmentId = 0;

roleId = 0;



  constructor(

    private employeeService:

      EmployeeService,

    private router: Router

  ) { }
  searchText = '';



  ngOnInit(): void {

    this.loadEmployees();

  }



  



  



  filteredEmployees() {



  return this.employees.filter(x =>



    (x.firstName + ' ' + x.lastName)

      .toLowerCase()

      .includes(

        this.searchText.toLowerCase()

      )



    ||



    x.email

      .toLowerCase()

      .includes(

        this.searchText.toLowerCase()

      )

      

  );



}

searchEmployee() {

  if(!this.searchName)
  {
    this.loadEmployees();
    return;
  }

  this.employeeService
      .searchByName(
        this.searchName
      )
      .subscribe((res:any)=>{

        this.employees = res;

      });
}

searchDepartment() {

  if(!this.departmentId)
  {
    this.loadEmployees();
    return;
  }

  this.employeeService
      .searchByDepartment(
        this.departmentId
      )
      .subscribe((res:any)=>{

        this.employees = res;

      });
}

searchRole() {

  if(!this.roleId)
  {
    this.loadEmployees();
    return;
  }

  this.employeeService
      .searchByRole(
        this.roleId
      )
      .subscribe((res:any)=>{

        this.employees = res;

      });
}



  deleteEmployee(id: number) {



    if (!confirm(

      'Delete Employee?'))

      return;



    this.employeeService

      .delete(id)

      .subscribe({

        next: () => {

          this.loadEmployees();

        }

      });

  }

  loadEmployees() {

  this.employeeService
    .getAll()
    .subscribe({

      next: (res: any) => {

        console.log('Employee Count:', res.length);
        console.log('Employee Response:', res);

        this.employees = res;

      },

      error: (err) => {

        console.log('API ERROR');
        console.log(err);

      }

    });
}


  addEmployee() {



    this.router.navigate(

      ['/employee-form']

    );

  }



  editEmployee(id: number) {



    this.router.navigate(

      ['/employee-form', id]

    );

  }

}