// src/app/auth/signup.component.ts
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';

@Component({
  standalone: true,
  selector: 'app-signup',
  imports: [ReactiveFormsModule, RouterModule, MatButtonModule, MatInputModule],
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {
  form : FormGroup;
  error = '';
  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router) {this.form = this.fb.group({
    email:    ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
  });}

  submit() {
    if (this.form.invalid) return;
    this.auth.signup(this.form.value as any).subscribe({
      next: () => this.router.navigate(['/login']),
      error: err => this.error = err.error
    });
  }
  
}
