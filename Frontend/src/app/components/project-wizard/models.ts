export interface Question {
  id: string;
  text: string;
  qrs: QrChoice[];
}

export interface QrChoice {
  id: string;
  name: string;
  dimension: string;
}

export interface Selection { qrId: string; selected: boolean; }
