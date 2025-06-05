import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { AsyncPipe, NgIf } from '@angular/common';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterModule, MatButtonModule, MatIconModule, NgIf, AsyncPipe],
  template: `
    <nav *ngIf="auth.logged$ | async"
         class="flex h-14 items-center bg-indigo-600 pl-6 text-white shadow">
      <a routerLink="/" class="mr-6 font-semibold text-lg">Decision Maps</a>
      <button mat-button routerLink="/wizard/new">New Map</button>
      <span class="flex-1"></span>
      <button mat-icon-button (click)="auth.logout()">
        <mat-icon>logout</mat-icon>
      </button>
    </nav>
  `,
})
export class NavbarComponent {
  constructor(public auth: AuthService) {}
}