import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { MainDashboardComponent } from './main-dashboard/main-dashboard.component';
import { EmployeeComponent } from './employee/employee.component';

const routes: Routes = [
  {path:'login'.toLocaleLowerCase(), component: LoginComponent },
  {path:'main-dashboard'.toLocaleLowerCase(), component: MainDashboardComponent },
  {path:'employee'.toLocaleLowerCase(), component: EmployeeComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' }, // domyślna ścieżka
  { path: '**', component: LoginComponent } // obsluga nieznanych sciezek
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
