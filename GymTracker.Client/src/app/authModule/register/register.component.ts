import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  email = '';
  password = '';
  firstName = '';
  lastName = '';
  phoneNumber = '';
  dateOfBirth: string = '';
  gender: number = 0;

  constructor(private authService: AuthService, private router: Router) {}

  onRegister() {
    this.authService.register({
      email: this.email,
      password: this.password,
      firstName: this.firstName,
      lastName: this.lastName,
      phoneNumber: this.phoneNumber,
      dateOfBirth: this.dateOfBirth,
      gender: this.gender
    }).subscribe({
      next: () => {
        alert('Registration successful! Please check your email to confirm your account.');
        this.router.navigate(['/login']);
      },
      error: err => {
        let errorMsg = 'Registration failed!';
        if (err.error && typeof err.error === 'string') {
          errorMsg = err.error;
        } else if (err.error && err.error.message) {
          errorMsg = err.error.message;
        }
        alert(errorMsg);
        console.error(err);
      }      
    });
  }
}
