import { Component, inject } from '@angular/core';
import { MatStepperModule } from '@angular/material/stepper';
import { ChecklistStepComponent } from './steps/checklist-step.component';
import { GraphStepComponent }     from './steps/graph-step.component';
import { MatrixStepComponent }    from './steps/dmatrix-step.component';

@Component({
  selector: 'app-project-wizard',
  standalone: true,
  imports: [MatStepperModule,
    ChecklistStepComponent,
    GraphStepComponent,
    MatrixStepComponent],
  templateUrl: './project-wizard.component.html',
  styleUrls: ['./project-wizard.component.css'],
})
export class ProjectWizardComponent {
  // later inject DecisionMapService for projectId
}