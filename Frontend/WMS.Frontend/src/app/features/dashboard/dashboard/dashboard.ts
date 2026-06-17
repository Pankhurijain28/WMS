import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatListModule } from '@angular/material/list';
import { BaseChartDirective } from 'ng2-charts';
import { ChartConfiguration, ChartData, ChartType } from 'chart.js';

import { NavbarComponent } from '../../../shared/layouts/navbar/navbar';
import { SidebarComponent } from '../../../shared/layouts/sidebar/sidebar';
import { DashboardService } from '../../../core/services/dashboard.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    NavbarComponent,
    SidebarComponent,
    MatCardModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatListModule,
    BaseChartDirective // Added for Charts
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class DashboardComponent implements OnInit {
  dashboard: any;
  isLoading = true;

  // Capstone Requirement: Charts/Graphs (Employee distribution)
  public pieChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: { position: 'right', labels: { color: '#f8fafc' } } // Styled for dark theme
    }
  };
  public pieChartData: ChartData<'pie', number[], string | string[]> = {
    labels: ['IT', 'HR', 'Finance', 'Engineering', 'Sales'],
    datasets: [ {
      data: [15, 5, 8, 20, 12], // Default mock data if API doesn't provide it
      backgroundColor: ['#2563eb', '#22c55e', '#f59e0b', '#ef4444', '#8b5cf6'],
      borderColor: '#1e293b'
    } ]
  };
  public pieChartType: ChartType = 'pie';

  // Capstone Requirement: Recent Activity Feed
  public recentActivities = [
    { message: 'John Doe joined the Engineering department.', time: '2 hours ago', icon: 'person_add' },
    { message: 'Leave request approved for Jane Smith.', time: '4 hours ago', icon: 'event_available' },
    { message: 'Q3 Financial Report uploaded.', time: '1 day ago', icon: 'upload_file' },
    { message: 'New project "Alpha" created.', time: '2 days ago', icon: 'work' }
  ];

  constructor(private service: DashboardService) {}

  ngOnInit(): void {

  console.log('Dashboard Loading...');

  this.service
    .getDashboardData()
    .subscribe({

      next: (res:any) => {

        console.log('Dashboard Success');
        console.log(res);

        this.dashboard = res;
        this.isLoading = false;
      },

      error: (err) => {

        console.log('Dashboard Error');
        console.log(err);

        this.isLoading = false;
      }

    });
}
}