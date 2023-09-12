import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { JwtAuth } from 'src/app/models/jwtAuth';
import { Login } from 'src/app/models/login';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginDto = new Login();
  jwtDto = new JwtAuth();

  constructor(private authService: AuthenticationService,private route:Router){}
    login(loginDto:Login){
      this.authService.login(loginDto).subscribe((jwtDto)=>{
        localStorage.setItem('jwtToken',jwtDto.token);
          this.route.navigate(['product/product-list']);
      });
    }

}
