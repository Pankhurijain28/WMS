import {
  Component,
  OnInit
} from '@angular/core';

import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

import { ProjectService }
from '../../../core/services/project.service';

import { NavbarComponent }
from '../../../shared/layouts/navbar/navbar';

import { SidebarComponent }
from '../../../shared/layouts/sidebar/sidebar';

@Component({
  selector: 'app-project-list',
  standalone: true,
  imports: [CommonModule, NavbarComponent, SidebarComponent],
  templateUrl: './project-list.html',
  styleUrl: './project-list.css'
})
export class ProjectListComponent
implements OnInit {

  projects: any[] = [];

  constructor(
    private service: ProjectService,
    private router: Router
  ) {}

  ngOnInit(): void {
  this.loadProjects();
}

loadProjects() {
  this.service.getAll().subscribe({
    next: (res:any) => {
      this.projects = res;
    },
    error: (err) => console.log(err)
  });
}

 addProject() {
  console.log('ADD PROJECT CLICKED');
  this.router.navigate(['/project-form']);
}

  editProject(id: number) {
    this.router.navigate(
      ['/project-form', id]
    );
  }

  deleteProject(id: number) {

    if (!confirm(
      'Delete Project?'))
      return;

    this.service
      .delete(id)
      .subscribe(() => {

        this.loadProjects();

      });
  }

  loadData() {

  console.log('PROJECT API CALL');

  this.service.getAll().subscribe({

    next: (res: any) => {

      console.log('PROJECT DATA');
      console.log(res);

      this.projects = res;

      console.log('PROJECT COUNT');
      console.log(this.projects.length);

    },

    error: (err) => {
      console.log(err);
    }

  });
}

}