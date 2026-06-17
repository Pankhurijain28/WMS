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

import { RoleService }
from '../../../core/services/role.service';

@Component({
  selector: 'app-role-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './role-form.html',
  styleUrl: './role-form.css'
})
export class RoleFormComponent
implements OnInit {

  roleForm!: FormGroup;

  roleId = 0;

  constructor(
    private fb: FormBuilder,
    private service: RoleService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {

    this.roleForm =
      this.fb.group({

        roleName: ['', Validators.required],
        description: ['']

      });

    this.roleId =
      Number(
        this.route.snapshot
          .paramMap.get('id')
      );

    if (this.roleId) {

      this.service
        .getById(
          this.roleId
        )
        .subscribe((res: any) => {

          this.roleForm
            .patchValue(res);

        });
    }
  }

  save() {

    if (this.roleForm.invalid) {
      this.roleForm.markAllAsTouched();
      this.toastr.warning('Please enter a role name.');
      return;
    }

    if (this.roleId) {

      this.service
        .update(this.roleId, this.roleForm.value)
        .subscribe({
          next: () => {
            this.toastr.success('Role updated successfully');
            this.router.navigate(['/roles']);
          },
          error: () => this.toastr.error('Failed to update role')
        });

      return;
    }

    this.service
      .create(this.roleForm.value)
      .subscribe({
        next: () => {
          this.toastr.success('Role created successfully');
          this.router.navigate(['/roles']);
        },
        error: () => this.toastr.error('Failed to create role')
      });
  }
}