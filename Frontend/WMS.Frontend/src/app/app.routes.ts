import { Routes } from '@angular/router';

import { authGuard } from './shared/guards/auth.guard';

import { LoginComponent }
from './features/auth/login/login';

import { DashboardComponent }
from './features/dashboard/dashboard/dashboard';

/* Employee */
import { EmployeeListComponent }
from './features/employees/employee-list/employee-list';

import { EmployeeFormComponent }
from './features/employees/employee-form/employee-form';

/* Department */
import { DepartmentListComponent }
from './features/departments/department-list/department-list';

import { DepartmentFormComponent }
from './features/departments/department-form/department-form';

/* Role */
import { RoleListComponent }
from './features/roles/role-list/role-list';

import { RoleFormComponent }
from './features/roles/role-form/role-form';

/* Client */
import { ClientListComponent }
from './features/clients/client-list/client-list';

import { ClientFormComponent }
from './features/clients/client-form/client-form';

/* Project */
import { ProjectListComponent }
from './features/projects/project-list/project-list';

import { ProjectFormComponent }
from './features/projects/project-form/project-form';

/* Allocation */
import { AllocationListComponent }
from './features/allocation/allocation-list/allocation-list';

import { AllocationFormComponent }
from './features/allocation/allocation-form/allocation-form';

/* Attendance */
import { AttendanceListComponent }
from './features/attendance/attendance-list/attendance-list';

/* Leave */
import { LeaveListComponent }
from './features/leaves/leave-list/leave-list';

/* Announcement */
import { AnnouncementListComponent }
from './features/announcements/announcement-list/announcement-list';

/* Audit */
import { AuditLogListComponent }
from './features/auditlogs/audit-log-list/audit-log-list';

export const routes: Routes = [

  {
    path: '',
    component: LoginComponent
  },

  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [authGuard]
  },

  /* Employees */

  {
    path: 'employees',
    component: EmployeeListComponent,
    canActivate: [authGuard]
  },

  {
    path: 'employee-form',
    component: EmployeeFormComponent,
    canActivate: [authGuard]
  },

  {
    path: 'employee-form/:id',
    component: EmployeeFormComponent,
    canActivate: [authGuard]
  },

  /* Departments */

  {
    path: 'departments',
    component: DepartmentListComponent,
    canActivate: [authGuard]
  },

  {
    path: 'department-form',
    component: DepartmentFormComponent,
    canActivate: [authGuard]
  },

  {
    path: 'department-form/:id',
    component: DepartmentFormComponent,
    canActivate: [authGuard]
  },

  /* Roles */

  {
    path: 'roles',
    component: RoleListComponent,
    canActivate: [authGuard]
  },

  {
    path: 'role-form',
    component: RoleFormComponent,
    canActivate: [authGuard]
  },

  {
    path: 'role-form/:id',
    component: RoleFormComponent,
    canActivate: [authGuard]
  },

  /* Clients */

  {
    path: 'clients',
    component: ClientListComponent,
    canActivate: [authGuard]
  },

  {
    path: 'client-form',
    component: ClientFormComponent,
    canActivate: [authGuard]
  },

  {
    path: 'client-form/:id',
    component: ClientFormComponent,
    canActivate: [authGuard]
  },

  /* Projects */

  {
    path: 'projects',
    component: ProjectListComponent,
    canActivate: [authGuard]
  },

  {
    path: 'project-form',
    component: ProjectFormComponent,
    canActivate: [authGuard]
  },

  {
    path: 'project-form/:id',
    component: ProjectFormComponent,
    canActivate: [authGuard]
  },
  

  /* Allocations */

  {
    path: 'allocations',
    component: AllocationListComponent,
    canActivate: [authGuard]
  },

  {
    path: 'allocation-form',
    component: AllocationFormComponent,
    canActivate: [authGuard]
  },

  /* Attendance */

  {
    path: 'attendance',
    component: AttendanceListComponent,
    canActivate: [authGuard]
  },

  /* Leaves */

  {
    path: 'leaves',
    component: LeaveListComponent,
    canActivate: [authGuard]
  },

  /* Announcements */

  {
    path: 'announcements',
    component: AnnouncementListComponent,
    canActivate: [authGuard]
  },

  /* Audit Logs */

  {
    path: 'auditlogs',
    component: AuditLogListComponent,
    canActivate: [authGuard]
  },

  {
    path: '**',
    redirectTo: ''
  }
];