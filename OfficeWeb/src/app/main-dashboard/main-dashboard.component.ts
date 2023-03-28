import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main-dashboard',
  templateUrl: './main-dashboard.component.html',
  styleUrls: ['./main-dashboard.component.css']
})
export class MainDashboardComponent {

constructor(private router: Router){}

navigateToComponent(){
  console.log(`Użytkownik próbuje przejść do zakładki "Pracownicy".`);
  this.router.navigate(['/employee'])
}

}
