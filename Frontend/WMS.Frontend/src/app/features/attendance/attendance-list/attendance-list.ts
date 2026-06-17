import {
  Component,
  OnInit
} from '@angular/core';

import { CommonModule }
from '@angular/common';

import { FormsModule } from '@angular/forms';

import { AttendanceService }
from '../../../core/services/attendance.service';

import { NavbarComponent }
from '../../../shared/layouts/navbar/navbar';

import { SidebarComponent }
from '../../../shared/layouts/sidebar/sidebar';

@Component({
  selector:'app-attendance-list',
  standalone:true,
  imports:[CommonModule, FormsModule, NavbarComponent, SidebarComponent],
  templateUrl:'./attendance-list.html'
})
export class AttendanceListComponent
implements OnInit{

  attendance:any[]=[];

  constructor(
    private service:
      AttendanceService
  ){}

  searchText = '';


  ngOnInit(): void {

    this.loadData();

  }

  loadData() {

  this.service
    .getAll()
    .subscribe((res:any)=>{

      console.log(res);

      this.attendance=res;

    });
}

  checkIn(){

    this.service
      .checkIn()
      .subscribe();
  }

  filteredAttendance() {

  console.log(this.attendance);

  return this.attendance.filter(x =>

    (x.employeeName || '')
      .toLowerCase()
      .includes(
        this.searchText.toLowerCase()
      )
  );
}

  checkOut(){

    this.service
      .checkOut()
      .subscribe();
  }

  loadAttendance() {

  this.service
    .getAll()
    .subscribe({

      next: (res:any) => {

        console.log(res);

        this.attendance = res;

      }

    });
}
}