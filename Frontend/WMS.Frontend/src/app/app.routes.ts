import { Routes } from '@angular/router';

import { authGuard } from './shared/guards/auth.guard';

import { LayoutComponent }
from './shared/layouts/layout/layout';

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

  /* Authenticated area — wrapped in the shared layout shell
     (sidebar + navbar are always visible) */
  {
    path: '',
    component: LayoutComponent,
    canActivate: [authGuard],
    children: [

      { path: 'dashboard', component: DashboardComponent },

      /* Employees */
      { path: 'employees', component: EmployeeListComponent },
      { path: 'employee-form', component: EmployeeFormComponent },
      { path: 'employee-form/:id', component: EmployeeFormComponent },

      /* Departments */
      { path: 'departments', component: DepartmentListComponent },
      { path: 'department-form', component: DepartmentFormComponent },
      { path: 'department-form/:id', component: DepartmentFormComponent },

      /* Roles */
      { path: 'roles', component: RoleListComponent },
      { path: 'role-form', component: RoleFormComponent },
      { path: 'role-form/:id', component: RoleFormComponent },

      /* Clients */
      { path: 'clients', component: ClientListComponent },
      { path: 'client-form', component: ClientFormComponent },
      { path: 'client-form/:id', component: ClientFormComponent },

      /* Projects */
      { path: 'projects', component: ProjectListComponent },
      { path: 'project-form', component: ProjectFormComponent },
      { path: 'project-form/:id', component: ProjectFormComponent },

      /* Allocations */
      { path: 'allocations', component: AllocationListComponent },
      { path: 'allocation-form', component: AllocationFormComponent },

      /* Attendance */
      { path: 'attendance', component: AttendanceListComponent },

      /* Leaves */
      { path: 'leaves', component: LeaveListComponent },

      /* Announcements */
      { path: 'announcements', component: AnnouncementListComponent },

      /* Audit Logs */
      { path: 'auditlogs', component: AuditLogListComponent }
    ]
  },

  {
    path: '**',
    redirectTo: ''
  }
];