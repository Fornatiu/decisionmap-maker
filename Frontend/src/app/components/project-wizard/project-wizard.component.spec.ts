import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectWizardComponent } from './project-wizard.component';

describe('ProjectWizardComponent', () => {
  let component: ProjectWizardComponent;
  let fixture: ComponentFixture<ProjectWizardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProjectWizardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProjectWizardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
