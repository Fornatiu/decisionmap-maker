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
  private readonly API = environment.SERVER_URL + '/projects';

  constructor(private http: HttpClient) {}

  /** Step 0 – create blank project */
  createProject(name: string): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(this.API, { projectName: name });
  }

  /** Step 1 – upsert checklist selections */
  upsertQrs(
    projectId: string,
    selections: QrSelectionDto[]
  ): Observable<void> {
    return this.http.put<void>(`${this.API}/${projectId}/qrs`, {
      selections,
    });
  }

  /** Step 2 – fetch matrix (nodes + edges) */
  getMatrix(projectId: string): Observable<MatrixDto> {
    return this.http.get<MatrixDto>(`${this.API}/${projectId}/matrix`);
  }

  /** Step 2 – set single cell */
  setEdge(
    projectId: string,
    payload: { fromQrId: string; toQrId: string; effect: string }
  ): Observable<void> {
    return this.http.patch<void>(`${this.API}/${projectId}/matrix`, payload);
  }

  /** Step 3 – get graph for elkjs/Sprotty */
  getGraph(projectId: string): Observable<GraphDto> {
    return this.http.get<GraphDto>(`${this.API}/${projectId}/graph`);
  }

  /** Dashboard helpers */
  getAllForUser(userId: string): Observable<DecisionMapDto[]> {
    return this.http.get<DecisionMapDto[]>(`${this.API}/user/${userId}`);
  }

  getById(projectId: string): Observable<DecisionMapDto> {
    return this.http.get<DecisionMapDto>(`${this.API}/${projectId}`);
  }
}