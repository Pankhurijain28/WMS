import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router'; // Add this import

import { NavbarComponent } from '../../../shared/layouts/navbar/navbar';
import { SidebarComponent } from '../../../shared/layouts/sidebar/sidebar';
import { AnnouncementService } from '../../../core/services/announcement.service';

@Component({
  selector: 'app-announcement-list',
  standalone: true,
  imports: [
    CommonModule, 
    FormsModule, 
    RouterModule, // Add it here
    NavbarComponent,
    SidebarComponent
  ],
  templateUrl: './announcement-list.html',
  styleUrl: './announcement-list.css'
})
export class AnnouncementListComponent implements OnInit {
  announcements: any[] = [];

  constructor(private service: AnnouncementService) {}

  ngOnInit(): void {
    this.service.getAll().subscribe({
      next: (res: any) => {
        this.announcements = res;
      },
      error: (err) => console.error(err)
    });
  }
}