import {
  Component,
  OnInit
} from '@angular/core';

import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule
} from '@angular/forms';

import { NavbarComponent }
from '../../../shared/layouts/navbar/navbar';

import { SidebarComponent }
from '../../../shared/layouts/sidebar/sidebar';

import { ActivatedRoute }
from '@angular/router';

import { CommonModule }
from '@angular/common';

import { ProjectService }
from '../../../core/services/project.service';

@Component({
  selector: 'app-project-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NavbarComponent,
    SidebarComponent
  ],
  templateUrl: './project-form.html',
  styleUrl: './project-form.css'
})
export class ProjectFormComponent
implements OnInit {

  projectForm!: FormGroup;

  projectId = 0;

  constructor(
    private fb: FormBuilder,
    private service: ProjectService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {

    this.projectForm = this.fb.group({
  projectName: [''],
  clientId: [0],
  startDate: [''],
  endDate: ['']
});

    this.projectId =
      Number(
        this.route.snapshot
          .paramMap.get('id')
      );

    if (this.projectId) {

      this.service
        .getById(
          this.projectId
        )
        .subscribe((res: any) => {

          this.projectForm
            .patchValue(res);

        });
    }
  }

  save() {

    if (this.projectId) {

      this.service
        .update(
          this.projectId,
          this.projectForm.value
        )
        .subscribe();

      return;
    }

    this.service
      .create(
        this.projectForm.value
      )
      .subscribe();
  }
}