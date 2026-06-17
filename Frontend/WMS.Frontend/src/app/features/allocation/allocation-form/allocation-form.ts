import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

import { AllocationService } from '../../../core/services/allocation.service';
import { NavbarComponent } from '../../../shared/layouts/navbar/navbar';
import { SidebarComponent } from '../../../shared/layouts/sidebar/sidebar';

@Component({
  selector: 'app-allocation-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    NavbarComponent,
    SidebarComponent
  ],
  templateUrl: './allocation-form.html'
})
export class AllocationFormComponent implements OnInit {
  allocationForm!: FormGroup;
  allocationId = 0;

  constructor(
    private fb: FormBuilder,
    private service: AllocationService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.allocationForm = this.fb.group({
      employeeId: ['', Validators.required],
      projectId: ['', Validators.required],
      role: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required]
    });

    this.allocationId = Number(this.route.snapshot.paramMap.get('id'));

    if (this.allocationId) {
      this.service.getById(this.allocationId).subscribe((res: any) => {
        if (res.startDate) res.startDate = res.startDate.split('T')[0];
        if (res.endDate) res.endDate = res.endDate.split('T')[0];
        this.allocationForm.patchValue(res);
      });
    }
  }

  save() {
    if (this.allocationForm.invalid) {
      alert('Please fill out all required fields.');
      return;
    }

    const payload = this.allocationForm.value;

    if (this.allocationId) {
      this.service.update(this.allocationId, payload).subscribe({
        next: () => {
          alert('Allocation Updated');
          this.router.navigate(['/allocations']);
        },
        error: (err) => console.error(err)
      });
    } else {
      this.service.create(payload).subscribe({
        next: () => {
          alert('Allocation Created');
          this.router.navigate(['/allocations']);
        },
        error: (err) => {
          console.error(err);
          alert('Failed to create. Check IDs.');
        }
      });
    }
  }
}