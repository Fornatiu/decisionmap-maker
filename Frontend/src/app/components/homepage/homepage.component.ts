import { Component, signal, computed } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import {
  trigger,
  state,
  style,
  transition,
  animate,
} from '@angular/animations';
import { MatButtonModule } from '@angular/material/button';
import { AsyncPipe, NgIf } from '@angular/common';
import { ProjectWizardComponent } from '../project-wizard/project-wizard.component';
import { map, Observable } from 'rxjs';

type ViewState = 'button' | 'expanding' | 'wizard';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [MatButtonModule, NgIf, AsyncPipe, ProjectWizardComponent],
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css'],
  animations: [
    trigger('panel', [
      state(
        'button',
        style({
          width: 'auto',
          height: 'auto',
          transform: 'scale(1)',
        })
      ),
      state(
        'expanding',
        style({
          width: '28rem',
          height: '16rem',
          transform: 'scale(1.5)',
        })
      ),
      state(
        'wizard',
        style({
          width: '90vw',
          height: '90vh',
          transform: 'scale(1)',
        })
      ),
      transition('button => expanding', animate('220ms ease-out')),
      transition('expanding => wizard', animate('240ms ease-out')),
    ]),
  ],
})
export class HomeComponent {
  view = signal<ViewState>('button');

  label!: Observable<string>;

  constructor(private auth: AuthService, private router: Router) {
    this.label = this.auth.logged$.pipe(
    map(logged => (logged ? 'Create new Decision Map' : 'Log in'))
  );}

  onClick() {
    let isLogged = false;
    this.auth.logged$.subscribe(logged => isLogged = logged);
    if (!isLogged) {
      this.router.navigate(['/login']);
      return;
    }
    this.view.set('expanding');
  }

  onDone(e: any) {
    if (e.toState === 'expanding') {
      this.view.set('wizard');
    }
  }
}