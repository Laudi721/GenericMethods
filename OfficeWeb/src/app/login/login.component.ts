// import { Component } from '@angular/core';

// @Component({
//   selector: 'app-login',
//   templateUrl: './login.component.html',
//   styleUrls: ['./login.component.css']
// })
// export class LoginComponent {

// }

import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  password: string = '';

  constructor(private http: HttpClient) {}


  onSubmit() {
    console.log(`Użytkownik ${this.username} próbuje się zalogować.`);
    // kod autentykacji użytkownika
    const endpoint = "http://localhost:5067/login";

    const user = {
      username: this.username,
      password: this.password
    };

    this.http.post(endpoint, user).subscribe(response => {
      console.log(response); // odpowiedź z serwera
    }, error => {
      console.error(error); // błąd podczas wywołania endpointu
      console.log(error.response.data)
    });
  }
}

