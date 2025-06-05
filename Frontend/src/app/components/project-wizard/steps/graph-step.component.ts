// steps/graph-step.component.ts
import { Component, EventEmitter, Output } from '@angular/core';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';

@Component({
  standalone: true,
  selector: 'app-graph-step',
  imports: [MatSelectModule, MatButtonModule],
  template: `
    <h3>Hybrid Decision Graph</h3>

    <!-- static preview image -->
    <img src="assets/graph-placeholder.svg" alt="graph" class="preview" />

    <mat-form-field appearance="outline">
      <mat-label>Default impact level</mat-label>
      <mat-select>
        <mat-option value="Immediate">Immediate</mat-option>
        <mat-option value="Enabling">Enabling</mat-option>
        <mat-option value="Systemic">Systemic</mat-option>
      </mat-select>
    </mat-form-field>

    <button mat-raised-button color="primary" (click)="completed.emit()">
      Next
    </button>
  `,
  styles: [
    `
      :host {
        display: flex;
        flex-direction: column;
        gap: 1.25rem;
        align-items: flex-start;
      }
      .preview {
        width: 100%;
        max-width: 640px;
        border: 1px solid #ddd;
      }
    `,
  ],
})
export class GraphStepComponent {
  @Output() completed = new EventEmitter<void>();
}
