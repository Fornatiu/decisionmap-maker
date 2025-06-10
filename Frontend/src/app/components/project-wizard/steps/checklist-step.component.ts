import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  inject,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  FormControl,
  ReactiveFormsModule,
} from '@angular/forms';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatRadioModule } from '@angular/material/radio';
import { forkJoin, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { QrMasterService } from '../../../services/qr-master.service';
import { DecisionMapService } from '../../../services/decision-map.service';
import {
  UpsertQrDto,
  QrMasterDto,
  QrSelectionDto,
} from '../../../models/decision-map/decision-map.module';

type Impact = 'Immediate' | 'Enabling' | 'Systemic';
interface QrControls {
  checked: FormControl<boolean>;
  impact: FormControl<Impact>;
}

@Component({
  selector: 'app-checklist-step',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatProgressBarModule,
    MatRadioModule,
    MatIconModule,
    MatTooltipModule,
    MatCardModule,
    MatDividerModule,
  ],
  templateUrl: './checklist-step.component.html',
  styleUrls: ['./checklist-step.component.css'],
})
export class ChecklistStepComponent implements OnInit, OnChanges {
  /* -----------------  DI  ----------------- */
  private qrSvc = inject(QrMasterService);
  private dmSvc = inject(DecisionMapService);
  private fb = inject(FormBuilder);

  /* -----------------  Inputs / Outputs  ----------------- */
  /** emit when the step can advance */
  @Output() completed = new EventEmitter<void>();

  /** allow the wizard to pass the real project id */
  @Input({ required: true }) projectId!: string;

  /* -----------------  State  ----------------- */
  loading = true;
  /** master catalogue grouped by dimension */
  qrs: QrMasterDto[] = [];
  /** ids already attached to the project */
  private projectQrIds = new Set<string>();

  groups: Array<[string, QrMasterDto[]]> = [];

  /** checkbox model: key = qrId, value = boolean */
  selections!: FormGroup<Record<string, FormGroup<QrControls>>>;

  /* -----------------  Lifecycle  ----------------- */

  ngOnChanges() {
    if (this.projectId) {
      this.load();
    }
  }

  private load() {
    forkJoin({
      masters: this.qrSvc.getAll(),
      projectQrs: this.dmSvc.getProjectQrs(this.projectId).pipe(
        // if the project is new, server might return 404/empty → convert to []
        catchError(() => of([] as QrSelectionDto[]))
      ),
    }).subscribe({
      next: ({ masters, projectQrs }) => {
        console.log('Masters loaded:', masters);
        this.qrs = masters;
        this.projectQrIds = new Set(projectQrs.map((p) => p.qrMasterId));

        /* build checkbox form with proper defaults */
        const cfg: Record<string, FormGroup<QrControls>> = {};
        masters.forEach((qr) => {
          cfg[qr.qrMasterID] = this.fb.group<QrControls>({
            checked: this.fb.control(this.projectQrIds.has(qr.qrMasterID), {
              nonNullable: true,
            }),
            impact: this.fb.control<Impact>('Immediate', { nonNullable: true }),
          });
        });

        console.log('Checkbox config:', cfg);

        if (this.selections) {
          // first reset, then add controls
          this.selections.reset();
          Object.entries(cfg).forEach(([key, ctrl]) =>
            this.selections.setControl(key, ctrl)
          );
        } else {
          this.selections = this.fb.group(cfg);
        }

        this.groups = this.buildGroups(masters);

        setTimeout(() => (this.loading = false), 600); // simulate loading delay
      },
      error: (err) => {
        console.error('Checklist init failed', err);
        this.loading = false;
      },
    });
  }

  ngOnInit() {}

  /* -----------------  UI helper  ----------------- */
  /** dimension → list of qrs (for *ngFor) */
  private buildGroups(list: QrMasterDto[]) {
    const byDim: Record<string, QrMasterDto[]> = {};
    list.forEach((qr) => (byDim[qr.dimension] ??= []).push(qr));
    return Object.entries(byDim);
  }

  /* -----------------  Continue logic  ----------------- */
  /**
   * → 1. build a list of *currently* checked ids
   * → 2. compare with what the project already had
   * → 3. only send (additions ∪ removals) to backend
   * → 4. emit `completed` when done
   */
  continue() {
    console.log('raw form value', this.selections.value);
    console.log('getRawValue()', this.selections.getRawValue());
    // 1) build an array of currently checked master IDs
    const nowCheckedIds = Object.entries(this.selections.getRawValue())
      .filter(([, v]) => v.checked)
      .map(([id]) => id);

    console.log('checked ids', nowCheckedIds);

    // 2) nothing changed? just move on
    if (nowCheckedIds.length === 0) {
      console.log('No QR selections changed, moving on');
      this.completed.emit();
      return;
    }

    const raw = this.selections.getRawValue();

    // 3) build the full payload: find the matching master and pluck its dimension
    const payload: UpsertQrDto[] = nowCheckedIds.map((qrMasterId) => {
      const master = this.qrs.find((q) => q.qrMasterID === qrMasterId)!;
      const impact  = raw[qrMasterId].impact;
      return {
        qrMasterId: master.qrMasterID,
        impactLevel: impact,   
        dimension: master.dimension as 'Tech | Econ | Social | Env',
      };
    });

    // 4) send it up
    this.dmSvc.upsertQrs(this.projectId, payload).subscribe({
      next: () => this.completed.emit(),
      error: (err) => console.error('Upsert failed', err),
    });
  }

  getFormGroup(id: string): FormGroup<QrControls> {
  const grp = this.selections.get(id) as FormGroup<QrControls> | null;
  if (!grp) {
    console.warn('Missing control group for', id);
    // create a dummy disabled group to avoid template null-errors
    return this.fb.group<QrControls>({
      checked: this.fb.control(false, {nonNullable:true}),
      impact : this.fb.control('Immediate', {nonNullable:true})
    });
  }
  return grp;
}
}
