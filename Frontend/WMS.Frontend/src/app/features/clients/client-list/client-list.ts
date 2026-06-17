import {
  Component,
  OnInit
} from '@angular/core';

import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

import { ClientService }
from '../../../core/services/client.service';

import { NavbarComponent }
from '../../../shared/layouts/navbar/navbar';

import { SidebarComponent }
from '../../../shared/layouts/sidebar/sidebar';

@Component({
  selector: 'app-client-list',
  standalone: true,
  imports: [CommonModule, NavbarComponent, SidebarComponent],
  templateUrl: './client-list.html',
  styleUrl: './client-list.css'
})
export class ClientListComponent
implements OnInit {

  clients: any[] = [];

  constructor(
    private service: ClientService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadClients();
  }

  loadClients() {

    this.service
      .getAll()
      .subscribe((res: any) => {

        this.clients = res;

      });
  }

  addClient() {
    this.router.navigate(
      ['/client-form']
    );
  }

  editClient(id: number) {
    this.router.navigate(
      ['/client-form', id]
    );
  }

  deleteClient(id: number) {

    if (!confirm(
      'Delete Client?'))
      return;

    this.service
      .delete(id)
      .subscribe(() => {

        this.loadClients();

      });
  }
}