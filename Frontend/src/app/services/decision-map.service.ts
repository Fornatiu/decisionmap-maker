import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import {
  QrSelectionDto,
  MatrixDto,
  GraphDto,
  DecisionMapDto,
} from '../models/decision-map/decision-map.module';

@Injectable({ providedIn: 'root' })
export class DecisionMapService {
  private readonly API = environment.SERVER_URL + '/DecisionMap';

  constructor(private http: HttpClient) {}

  createProject(projectName: string, ownerUserId: string): Observable<string> {
    return this.http.post<string>(`${this.API}/add`, { ownerUserId,projectName });
  }

  upsertQrs(
    projectId: string,
    selections: QrSelectionDto[]
  ): Observable<void> {
    return this.http.post<void>(`${this.API}/project`, {
      projectId,
      selections,
    });
  }
  getMatrix(projectId: string): Observable<MatrixDto> {
    return this.http.get<MatrixDto>(`${this.API}/${projectId}/matrix`);
  }

  getProjectQrs(projectId: string): Observable<QrSelectionDto[]> {
    return this.http.get<QrSelectionDto[]>(`${this.API}/projectget/${projectId}`);
  }

  setEdge(
    projectId: string,
    payload: { fromQrId: string; toQrId: string; effect: string }
  ): Observable<void> {
    return this.http.patch<void>(`${this.API}/${projectId}/matrix`, payload);
  }

  getGraph(projectId: string): Observable<GraphDto> {
    return this.http.get<GraphDto>(`${this.API}/${projectId}/graph`);
  }

  getAllForUser(userId: string): Observable<DecisionMapDto[]> {
    return this.http.get<DecisionMapDto[]>(`${this.API}/user/${userId}`);
  }

  getById(projectId: string): Observable<DecisionMapDto> {
    return this.http.get<DecisionMapDto>(`${this.API}/${projectId}`);
  }
}