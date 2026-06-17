import {
  Component,
  OnInit
} from '@angular/core';

import { CommonModule }
from '@angular/common';

import { LeaveService }
from '../../../core/services/leave.service';

import { NavbarComponent }
from '../../../shared/layouts/navbar/navbar';

import { SidebarComponent }
from '../../../shared/layouts/sidebar/sidebar';

@Component({
  selector:'app-leave-list',
  standalone:true,
  imports:[CommonModule, NavbarComponent, SidebarComponent],
  templateUrl:'./leave-list.html'
})
export class LeaveListComponent
implements OnInit{

  leaves:any[]=[
  {
    leaveId:999,
    employeeName:'TEST USER',
    leaveType:'TEST LEAVE',
    fromDate:'2026-06-20',
    toDate:'2026-06-21',
    status:'Pending'
  }
];

  constructor(
    private service:
      LeaveService
  ){}

  

  ngOnInit(): void {

    console.log('LEAVE COMPONENT LOADED');
    next:(res:any)=>{

  console.log('LEAVE DATA');
  console.log(res);

    this.leaves = res;

  console.log('AFTER ASSIGN');
  console.log(this.leaves.length);
}

  this.loadData();

    
  }
 loadData() {

  console.log('CALLING LEAVE API');

  this.service.getAll()
    .subscribe({

      next: (res:any) => {

        console.log('LEAVE DATA');
        console.log(res);

        this.leaves = [...res];

        console.log('AFTER ASSIGN');
        console.log(this.leaves.length);

      },

      error: (err) => {

        console.log('LEAVE ERROR');
        console.log(err);

      }

    });
}
filteredLeaves() {
  console.log(this.leaves);
  return this.leaves;
}
}