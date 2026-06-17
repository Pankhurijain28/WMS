import {
  Component,
  OnInit
} from '@angular/core';

import {
  CommonModule
} from '@angular/common';

import {
  FormsModule
} from '@angular/forms';

import {
  AuditLogService
} from '../../../core/services/audit-log.service';

@Component({
  selector: 'app-audit-log-list',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './audit-log-list.html',
  styleUrl: './audit-log-list.css'
})
export class AuditLogListComponent
implements OnInit {

  logs: any[] = [];

  searchText = '';

  constructor(
    private service:
      AuditLogService
  ) {}

  ngOnInit(): void {

    this.loadLogs();

  }

  loadLogs(): void {

    this.service
      .getAll()
      .subscribe({
        next: (res: any) => {

          this.logs = res;

        },

        error: (err) => {

          console.error(err);

        }
      });

  }

  filteredLogs() {

    return this.logs.filter(x =>

      x.entityName
        .toLowerCase()
        .includes(
          this.searchText.toLowerCase()
        )

      ||

      x.createdBy
        .toLowerCase()
        .includes(
          this.searchText.toLowerCase()
        )

      ||

      x.action
        .toLowerCase()
        .includes(
          this.searchText.toLowerCase()
        )
    );

  }

}