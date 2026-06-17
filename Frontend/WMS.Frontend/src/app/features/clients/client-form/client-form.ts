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

import { ClientService }
from '../../../core/services/client.service';

import { NavbarComponent }
from '../../../shared/layouts/navbar/navbar';

import { SidebarComponent }
from '../../../shared/layouts/sidebar/sidebar';

@Component({
  selector: 'app-client-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NavbarComponent,
    SidebarComponent
  ],
  templateUrl: './client-form.html',
  styleUrl: './client-form.css'
})
export class ClientFormComponent
implements OnInit {

  clientForm!: FormGroup;

  clientId = 0;

  constructor(
    private fb: FormBuilder,
    private service: ClientService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {

    this.clientForm = this.fb.group({
  clientName: [''],
  clientAddress: [''],
  clientPhoneNumber: [''],
  clientLocation: ['']
});

    this.clientId =
      Number(
        this.route.snapshot
          .paramMap.get('id')
      );

    if (this.clientId) {

      this.service
        .getById(
          this.clientId
        )
        .subscribe((res: any) => {

          this.clientForm
            .patchValue(res);

        });
    }
  }

  save() {

    if (this.clientId) {

      this.service
        .update(
          this.clientId,
          this.clientForm.value
        )
        .subscribe();

      return;
    }

    this.service
      .create(
        this.clientForm.value
      )
      .subscribe();
  }
}