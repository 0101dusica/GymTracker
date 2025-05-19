import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import Swal from 'sweetalert2';
import { ButtonComponent } from '../../sharedModule/button/button.component';
import { InputFieldComponent } from '../../sharedModule/input-field/input-field.component';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, InputFieldComponent, ButtonComponent, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  email: string = '';
  password: string = '';

  loading: boolean = false;

  constructor(private authService: AuthService, private router: Router) {}

  onLogin() {
    this.loading = true;
    this.authService.login(this.email, this.password).subscribe({
      next: () => {
        Swal.fire({
          title: 'Success!',
          text: `Your login was successful!`,
          icon: 'success',
          confirmButtonText: 'OK',
          confirmButtonColor: '#5f99be',
          showConfirmButton: true,
          timer: 3000,
        });
        this.loading = false;
        this.router.navigate(['/workouts']);
      },
      error: err => {
        Swal.fire({
          title: 'Login Failed',
          text: 'Please fill out all fields correctly.',
          icon: 'error',
          confirmButtonText: 'OK',
          confirmButtonColor: '#5f99be',
  
        });
        console.error(err);
        
        this.loading = false;
      }
    });
  }
}
