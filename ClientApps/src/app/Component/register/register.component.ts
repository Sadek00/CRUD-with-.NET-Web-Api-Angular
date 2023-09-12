import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { JwtAuth } from 'src/app/models/jwtAuth';
import { Register } from 'src/app/models/register';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerDto = new Register();
  jwtDto = new JwtAuth();

  constructor(private authService: AuthenticationService,private router:Router){}
    register(registerDto:Register){
      this.authService.register(registerDto).subscribe((product)=>{
        this.router.navigate(['product/product-list']);
      });
    }
}
