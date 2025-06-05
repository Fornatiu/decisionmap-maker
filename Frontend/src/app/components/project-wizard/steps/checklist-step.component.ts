// import { Component, EventEmitter, Output } from '@angular/core';
// import { CommonModule } from '@angular/common';
// import { MatStepperModule } from '@angular/material/stepper';
// import { MatCheckboxModule } from '@angular/material/checkbox';
// import { MatButtonModule } from '@angular/material/button';
// import { AbstractControl, FormBuilder, FormControl, ReactiveFormsModule } from '@angular/forms';
// import { Question } from '../models';

// @Component({
//   standalone: true,
//   selector: 'app-checklist-step',
//   imports: [
//     CommonModule,
//     ReactiveFormsModule,
//     MatStepperModule,
//     MatCheckboxModule,
//     MatButtonModule,
//   ],
//   templateUrl: './checklist-step.component.html',
//   styleUrls: ['./checklist-step.component.css'],
// })
// export class ChecklistStepComponent {
//   @Output() completed = new EventEmitter<void>();

//   fb: FormBuilder = new FormBuilder();

//   /** Fake data until we call QrMasterService */
//   questions: Question[] = [
//     {
//       id: 'q1',
//       text: 'Question 1 – Does the project address …?',
//       qrs: [
//         { id: 'qr1', name: 'Performance', dimension: 'Tech' },
//         { id: 'qr2', name: 'Maintainability', dimension: 'Tech' },
//       ],
//     },
//     {
//       id: 'q2',
//       text: 'Question 2 – Which economic aspects …?',
//       qrs: [
//         { id: 'qr3', name: 'Development Cost', dimension: 'Econ' },
//         { id: 'qr4', name: 'Licensing Fees',   dimension: 'Econ' },
//       ],
//     },
//     {
//       id: 'q3',
//       text: 'Question 3 – Social / environmental …?',
//       qrs: [
//         { id: 'qr5', name: 'Accessibility', dimension: 'Soc' },
//         { id: 'qr6', name: 'Energy Usage',  dimension: 'Env' },
//       ],
//     },
//   ];

//   /** track selections per question */
//   selections = this.fb.group(
//     Object.fromEntries(
//       this.questions.map((q) => [
//         q.id,
//         this.fb.group(
//           Object.fromEntries(q.qrs.map((qr) => [qr.id, false]))
//         ),
//       ])
//     )
//   );

//   constructor(fb: FormBuilder) { this.fb = fb; }

//   done = this.fb.control(false);

//   finish() {
//     // gather selected QRs
//     const raw = this.selections.value;
//     const chosenIds = Object.values(raw)
//       .flatMap((g: any) =>
//         Object.entries(g)
//           .filter(([_, v]) => v)
//           .map(([id]) => id)
//       );

//     console.log('Selected QRs:', chosenIds);
//     // TODO: call DecisionMapService.upsertQrs(projectId, chosenIds)

//     this.completed.emit();
//   }

//    fieldCtrl(qId: string, qrId: string): FormControl {
//   return this.selections.get(qId)!.get(qrId)! as FormControl;
//   }

//     stepCtrl(qId: string): AbstractControl {
//   return this.selections.get(qId)!;          // for [stepControl]
//   }
  
// }


import {
  Component,
  EventEmitter,
  OnInit,
  Output,
  inject,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, FormControl } from '@angular/forms';
import {
  MatCheckboxModule,
  MatCheckboxChange,
} from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { QrMasterService, DecisionMapService, QrMaster } from '../../services';

@Component({
  standalone: true,
  selector: 'app-checklist-step',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatButtonModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './checklist-step.component.html',
  styleUrls: ['./checklist-step.component.css'],
})
export class ChecklistStepComponent implements OnInit {
  private qrSvc = inject(QrMasterService);
  private dmSvc = inject(DecisionMapService);
  private fb = inject(FormBuilder);

  /** emits when successfully saved */
  @Output() completed = new EventEmitter<void>();

  loading = true;
  qrs: QrMaster[] = [];

  // track selection in a simple form group: key = qrId, value = boolean
  selections = this.fb.group<{ [id: string]: FormControl<boolean> }>({});

  /** fake projectId for demo – pass it in via @Input in real wizard */
  projectId = '00000000-0000-0000-0000-000000000001';

  ngOnInit() {
    this.qrSvc.getAll().subscribe({
      next: (list) => {
        this.qrs = list;
        const cfg: Record<string, FormControl<boolean>> = {};
        list.forEach((qr) => (cfg[qr.id] = this.fb.control(false)));
        this.selections = this.fb.group(cfg);
        this.loading = false;
      },
      error: () => (this.loading = false),
    });
  }

  /** save & advance */
  continue() {
    const chosen: UpsertQrDto[] = Object.entries(this.selections.value)
      .filter(([_, checked]) => checked)
      .map(([qrId]) => ({
        qrMasterId: qrId,
        impactLevel: 'Immediate' as const, // default; will adjust in Graph step
      }));

    if (chosen.length === 0) {
      this.completed.emit(); // nothing selected; still move on
      return;
    }

    this.dmSvc.upsertQrs(this.projectId, chosen).subscribe({
      next: () => this.completed.emit(),
      error: (err) => console.error(err),
    });
  }

  /* helper to show dimensions as section headers */
  dimGroups() {
    const dims: Record<string, QrMaster[]> = {};
    this.qrs.forEach((qr) => {
      (dims[qr.dimension] ??= []).push(qr);
    });
    return Object.entries(dims);
  }
}
