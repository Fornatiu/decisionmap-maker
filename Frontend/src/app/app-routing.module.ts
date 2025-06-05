import { Routes } from '@angular/router';
import { HomeComponent } from './components/homepage/homepage.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: 'login',
    loadComponent: () =>
      import('./components/login/login.component').then(m => m.LoginComponent),
  },
  {
    path: 'signup',
    loadComponent: () =>
      import('./components/signup/signup.component').then(m => m.SignupComponent),
  },
  // {
  //   path: 'wizard/:projectId',
  //   loadComponent: () =>
  //     import('./wizard/project-wizard.component').then(m => m.ProjectWizardComponent),
  // },
  { path: '**', redirectTo: '' },
];
