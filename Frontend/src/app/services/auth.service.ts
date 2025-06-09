import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '../environments/environment';

export interface AuthDto { token: string }
export interface Credentials { email: string; password: string }

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly API = `${environment.SERVER_URL}/Authentication`;
  private readonly key = 'token';

  private _logged$ = new BehaviorSubject<boolean>(!!localStorage.getItem(this.key));
  readonly logged$  = this._logged$.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  login(c: Credentials): Observable<AuthDto> {
    return this.http.post<AuthDto>(`${this.API}/login`, c).pipe(
      tap(r => { localStorage.setItem(this.key, r.token); this._logged$.next(true); })
    );
  }

  signup(cred: Credentials): Observable<any> {
    return this.http.post<AuthDto>(`${this.API}/register`, cred);
  }

  logout() { localStorage.removeItem(this.key); this._logged$.next(false); this.router.navigate(['/']); }
  token()  { return localStorage.getItem(this.key); }

  getUserIdFromToken(): string {
    const token = localStorage.getItem('token');
    if (token) {
      try {
        const payloadBase64 = token.split('.')[1]; 
        const decodedPayload = JSON.parse(atob(payloadBase64)); 
        return decodedPayload.nameid || ''; 
      } catch (error) {
        console.error('Error parsing JWT token:', error);
        return '';
      }
    }
    return '';
  }
}