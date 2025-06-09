import { Component, inject, OnInit } from '@angular/core';
import { MatStepperModule } from '@angular/material/stepper';
import { ChecklistStepComponent } from './steps/checklist-step.component';
import { GraphStepComponent }     from './steps/graph-step.component';
import { MatrixStepComponent }    from './steps/dmatrix-step.component';
import { DecisionMapService } from '../../services/decision-map.service';
import { AuthService } from '../../services/auth.service';

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
export class ProjectWizardComponent implements OnInit {
  public projectId: string = '';
  private dmSvc = inject(DecisionMapService);
  private authSvc = inject(AuthService);


  ngOnInit() {
    var userId = this.authSvc.getUserIdFromToken();
    this.dmSvc.createProject('New Project3',userId).subscribe({
      next: (res) => {
        console.log('Project created successfully:', res);
        this.projectId = res;
      },
      error: (err) => {
        console.error('Error creating project:', err);
      }
    });
  }
   
}