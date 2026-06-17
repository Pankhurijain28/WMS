import {
  Component,
  OnInit
} from '@angular/core';

import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { ActivatedRoute, Router }
from '@angular/router';

import { CommonModule }
from '@angular/common';

import { ToastrService }
from 'ngx-toastr';

import { DepartmentService }
from '../../../core/services/department.service';

@Component({
  selector: 'app-department-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './department-form.html',
  styleUrl: './department-form.css'
})
export class DepartmentFormComponent
implements OnInit {

  departmentForm!: FormGroup;

  departmentId = 0;

  constructor(
    private fb: FormBuilder,
    private service:
      DepartmentService,
    private route:
      ActivatedRoute,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {

    this.departmentForm =
      this.fb.group({

        departmentName: ['', Validators.required],
        description: ['']

      });

    this.departmentId =
      Number(
        this.route.snapshot
          .paramMap.get('id')
      );

    if (this.departmentId) {

      this.service
        .getById(
          this.departmentId
        )
        .subscribe((res: any) => {

          this.departmentForm
            .patchValue(res);

        });
    }
  }

  save() {

    if (this.departmentForm.invalid) {
      this.departmentForm.markAllAsTouched();
      this.toastr.warning('Please enter a department name.');
      return;
    }

    if (this.departmentId) {

      this.service
        .update(this.departmentId, this.departmentForm.value)
        .subscribe({
          next: () => {
            this.toastr.success('Department updated successfully');
            this.router.navigate(['/departments']);
          },
          error: () => this.toastr.error('Failed to update department')
        });

      return;
    }

    this.service
      .create(this.departmentForm.value)
      .subscribe({
        next: () => {
          this.toastr.success('Department created successfully');
          this.router.navigate(['/departments']);
        },
        error: () => this.toastr.error('Failed to create department')
      });
  }
}