import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AllocationService } from '../../../core/services/allocation.service';
import { NavbarComponent } from '../../../shared/layouts/navbar/navbar';
import { SidebarComponent } from '../../../shared/layouts/sidebar/sidebar';

@Component({
  selector: 'app-allocation-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    NavbarComponent,
    SidebarComponent
  ],
  templateUrl: './allocation-list.html'
})
export class AllocationListComponent implements OnInit {
  allocations: any[] = [];

  constructor(private service: AllocationService) {}

  ngOnInit(): void {
    this.loadAllocations();
  }

  loadAllocations() {
    this.service.getAll().subscribe({
      next: (res: any) => {
        this.allocations = res;
      },
      error: (err) => console.error(err)
    });
  }

  delete(id: number) {
    if (confirm('Are you sure you want to remove this allocation?')) {
      this.service.delete(id).subscribe({
        next: () => this.loadAllocations(),
        error: (err) => {
          console.error(err);
          alert('Delete failed');
        }
      });
    }
  }
}