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

import { RoleService }
from '../../../core/services/role.service';

import { NavbarComponent }
from '../../../shared/layouts/navbar/navbar';

import { SidebarComponent }
from '../../../shared/layouts/sidebar/sidebar';

@Component({
  selector: 'app-role-form',
  standalone: true,
  imports: [
    CommonModule,
    NavbarComponent,
    SidebarComponent,
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
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {

    this.roleForm =
      this.fb.group({

        roleName: [''],
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

    if (this.roleId) {

      this.service
        .update(
          this.roleId,
          this.roleForm.value
        )
        .subscribe();

      return;
    }

    this.service
      .create(
        this.roleForm.value
      )
      .subscribe();
  }
}