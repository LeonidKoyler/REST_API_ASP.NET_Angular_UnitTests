import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import {HeaderComponent} from './header/header.component'
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { CalculationComponent } from './calculation/calculation.component';
import { RequirementsComponent } from './requirements/requirements.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    CalculationComponent,
    RequirementsComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: 'calculation', component: CalculationComponent },
      { path: 'requirements', component: RequirementsComponent },
      { path: '', redirectTo: 'calculation', pathMatch: 'full' },
      { path: '**', component: CalculationComponent } // page not found (will not be implemented)
    ]),
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
