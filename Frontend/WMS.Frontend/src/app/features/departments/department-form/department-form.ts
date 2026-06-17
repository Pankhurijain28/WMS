import {
  Component,
  OnInit
} from '@angular/core';

import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule
} from '@angular/forms';

import { ActivatedRoute }
from '@angular/router';

import { CommonModule }
from '@angular/common';

import { DepartmentService }
from '../../../core/services/department.service';
import { NavbarComponent }
from '../../../shared/layouts/navbar/navbar';

import { SidebarComponent }
from '../../../shared/layouts/sidebar/sidebar';

@Component({
  selector: 'app-department-form',
  standalone: true,
  imports: [
    CommonModule,
    NavbarComponent,
  SidebarComponent,
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
      ActivatedRoute
  ) {}

  ngOnInit(): void {

    this.departmentForm =
      this.fb.group({

        departmentName: [''],
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

  console.log('SAVE CLICKED');

  console.log(this.departmentForm.value);

  this.service
    .create(this.departmentForm.value)
    .subscribe({

      next: (res) => {

        console.log('SUCCESS');
        console.log(res);

      },

      error: (err) => {

        console.log('ERROR');
        console.log(err);

      }

    });

}
}