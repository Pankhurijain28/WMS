import { Component, OnInit } from '@angular/core';

import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';

import { ActivatedRoute, Router } from '@angular/router';

import { CommonModule } from '@angular/common';

import { EmployeeService } from '../../../core/services/employee.service';

import { SidebarComponent } from '../../../shared/layouts/sidebar/sidebar';

import { NavbarComponent } from '../../../shared/layouts/navbar/navbar';



@Component({

  selector: 'app-employee-form',

  standalone: true,

  imports: [

    CommonModule,

    ReactiveFormsModule,

    NavbarComponent,

    SidebarComponent

  ],

  templateUrl: './employee-form.html',

  styleUrl: './employee-form.css'

})

export class EmployeeFormComponent implements OnInit {

  employeeForm!: FormGroup;

  employeeId = 0;

  isSubmitting = false; // Add loading state



  constructor(

    private fb: FormBuilder,

    private service: EmployeeService,

    private route: ActivatedRoute,

    private router: Router // Useful for redirecting after save

  ) { }



  ngOnInit(): void {

    // 1. Added Validators to prevent empty submissions

    this.employeeForm = this.fb.group({

      firstName: ['', [Validators.required, Validators.maxLength(50)]],

      lastName: ['', [Validators.required, Validators.maxLength(50)]],

      email: ['', [Validators.required, Validators.email]],

      phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],

      departmentId: [null, Validators.required], // Changed from 1 to null to force user selection

      roleId: [null, Validators.required],

      gender: ['M', Validators.required],

      dob: [new Date().toISOString().split('T')[0], Validators.required],

      doj: [new Date().toISOString().split('T')[0], Validators.required],

      status: ['Active', Validators.required]

    });



    this.employeeId = Number(this.route.snapshot.paramMap.get('id'));



    if (this.employeeId) {

      this.service.getById(this.employeeId).subscribe((res: any) => {

        // Use patchValue, but format dates correctly if they come back as full DateTimes from .NET

        if (res.dob) res.dob = res.dob.split('T')[0];

        if (res.doj) res.doj = res.doj.split('T')[0];

        this.employeeForm.patchValue(res);

      });

    }

  }



  save() {
    console.log(this.employeeForm.value);

  console.log(this.employeeForm.valid);

  console.log(this.employeeForm.errors);

  console.log(this.employeeForm);

    // 2. Stop execution if form is invalid

    if (this.employeeForm.invalid) {

      this.employeeForm.markAllAsTouched(); // Highlights all errors in UI

      alert('Please fill out all required fields correctly.');

      return;

    }



    this.isSubmitting = true;

    const payload = this.employeeForm.value;



    // Ensure IDs are numbers just in case the HTML input casted them to strings

    payload.departmentId = Number(payload.departmentId);

    payload.roleId = Number(payload.roleId);



    if (this.employeeId) {

      this.service.update(this.employeeId, payload).subscribe({

        next: () => {

          alert('Employee Updated Successfully!');

          this.router.navigate(['/employees']); // Redirect to list

        },

        error: (err) => {

          console.error(err);

          alert('Update Failed: ' + (err.error?.title || err.message));

          this.isSubmitting = false;

        }

      });

    } else {

      this.service.create(payload).subscribe({

        next: () => {

          alert('Employee Created Successfully!');

          this.router.navigate(['/employees']); // Redirect to list

        },

        error: (err) => {

          console.error('FULL ERROR:', err);

          // 3. Better Error Parsing to read .NET ValidationProblemDetails

          let errorMessage = 'Creation Failed.';

          if (err.error && err.error.errors) {

            errorMessage += '\n' + JSON.stringify(err.error.errors);

          } else if (err.error) {

            errorMessage += '\n' + JSON.stringify(err.error);

          }

          alert(errorMessage);

          this.isSubmitting = false;

        }

      });

    }

  }

}