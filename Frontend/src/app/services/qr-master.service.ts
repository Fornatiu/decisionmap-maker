// src/app/services/qr-master.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { QrMasterDto } from '../models/decision-map/decision-map.module';

@Injectable({ providedIn: 'root' })
export class QrMasterService {
  private readonly API = environment.SERVER_URL + '/QrMaster/get';

  constructor(private http: HttpClient) {}

  getAll(): Observable<QrMasterDto[]> {
    return this.http.get<QrMasterDto[]>(this.API);
  }
}
