import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

import { routes } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/homepage/homepage.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { ProjectWizardComponent } from './components/project-wizard/project-wizard.component';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    RouterModule.forRoot(routes),
    MatButtonModule,
    MatIconModule,
    NavbarComponent,           // standalone
    HomeComponent,             // standalone
    ProjectWizardComponent,    // standalone
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
