import {
  Component,
  OnInit
} from '@angular/core';

import { CommonModule }
from '@angular/common';

import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { ToastrService }
from 'ngx-toastr';

import { LeaveService }
from '../../../core/services/leave.service';

import { AuthService }
from '../../../core/services/auth.service';

@Component({
  selector: 'app-leave-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './leave-list.html'
})
export class LeaveListComponent
implements OnInit {

  leaves: any[] = [];

  leaveForm!: FormGroup;
  showForm = false;

  constructor(
    private service: LeaveService,
    private fb: FormBuilder,
    private toastr: ToastrService,
    public auth: AuthService
  ) {}

  ngOnInit(): void {

    this.leaveForm = this.fb.group({
      empId: [null, Validators.required],
      leaveType: ['Sick', Validators.required],
      fromDate: ['', Validators.required],
      toDate: ['', Validators.required],
      reason: ['']
    });

    this.loadData();
  }

  loadData() {
    this.service.getAll().subscribe({
      next: (res: any) => {
        this.leaves = res;
      },
      error: () => {
        this.toastr.error('Failed to load leaves');
      }
    });
  }

  toggleForm() {
    this.showForm = !this.showForm;
  }

  applyLeave() {

    if (this.leaveForm.invalid) {
      this.leaveForm.markAllAsTouched();
      this.toastr.warning('Please fill all required fields.');
      return;
    }

    this.service.create(this.leaveForm.value).subscribe({
      next: () => {
        this.toastr.success('Leave applied successfully');
        this.showForm = false;
        this.leaveForm.reset({ leaveType: 'Sick' });
        this.loadData();
      },
      error: (err) => {
        this.toastr.error(
          err.error?.title || 'Failed to apply leave'
        );
      }
    });
  }

  approve(id: number) {
    this.service.approve(id).subscribe({
      next: () => {
        this.toastr.success('Leave approved');
        this.loadData();
      },
      error: () => this.toastr.error('Failed to approve leave')
    });
  }

  reject(id: number) {
    this.service.reject(id).subscribe({
      next: () => {
        this.toastr.info('Leave rejected');
        this.loadData();
      },
      error: () => this.toastr.error('Failed to reject leave')
    });
  }

  cancel(id: number) {
    this.service.cancel(id).subscribe({
      next: () => {
        this.toastr.info('Leave cancelled');
        this.loadData();
      },
      error: () => this.toastr.error('Failed to cancel leave')
    });
  }
}
