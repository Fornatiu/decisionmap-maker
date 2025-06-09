
export interface QrSelectionDto {
  qrMasterId: string;
  impactLevel: string;
  dimension: string;
}

export interface MatrixEdgeDto {
  fromId: string;
  toId: string;
  effect: string;
}

export interface MatrixDto {
  nodes: QrSelectionDto[];
  edges: MatrixEdgeDto[];
}

export interface GraphDto {
  nodes: unknown[];
  edges: unknown[];
}

export interface DecisionMapDto {
  id: string;
  name: string;
  createdAt: string;
  qrCount: number;
}

// models/qr-master.models.ts
export interface QrMasterDto {
  qrMasterID: string;
  name: string;
  dimension: string;
}

export interface UpsertQrDto {
  qrMasterId: string;
  impactLevel: 'Immediate' | 'Enabling' | 'Systemic';
  dimension: 'Tech | Econ | Social | Env';
}
