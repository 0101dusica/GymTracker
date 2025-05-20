import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ButtonComponent } from '../../sharedModule/button/button.component';
import { InputFieldComponent } from '../../sharedModule/input-field/input-field.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, InputFieldComponent, ButtonComponent, RouterModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  email = '';
  password = '';
  repeatPassword = '';
  firstName = '';
  lastName = '';
  phoneNumber = '';
  dateOfBirth: string = '';
  gender: number = 0;

  loading: boolean = false;

  constructor(private authService: AuthService, private router: Router) {}

  onRegister() {
    this.loading = true;

    if (!this.areFieldsFilled()) return;
    if (!this.doPasswordsMatch()) return;
    if (!this.isPhoneNumberValid()) return;
  
    this.performRegistration();
  }

  private areFieldsFilled(): boolean {
    const requiredFields = [
      this.email, this.password, this.repeatPassword,
      this.firstName, this.lastName,
      this.phoneNumber, this.dateOfBirth
    ];
    console.log('Field values:', requiredFields);

    const allFilled = requiredFields.every(field => field.trim() !== '');
  
    if (!allFilled) {
      Swal.fire({
        title: 'Invalid input',
        text: 'Please fill in all required fields correctly.',
        icon: 'warning',
        confirmButtonColor: '#f0ad4e'
      });
      this.loading = false;
      return false;
    }
  
    return true;
  }

  private isPhoneNumberValid(): boolean {
    const phoneRegex = /^\+?[0-9]+$/;

    if (!phoneRegex.test(this.phoneNumber)) {
      Swal.fire({
        title: 'Invalid phone number',
        text: 'Phone number must contain only digits and may optionally start with +.',
        icon: 'warning',
        confirmButtonText: 'OK',
        confirmButtonColor: '#f0ad4e',
      });
      this.loading = false;
      return false;
    }
    return true;
  }
  

  private doPasswordsMatch(): boolean {
    if (this.password !== this.repeatPassword) {
      Swal.fire({
        title: 'Password mismatch',
        text: 'Passwords do not match. Please check and try again.',
        icon: 'error',
        confirmButtonText: 'OK',
        confirmButtonColor: '#ff0f0f',
      });
      this.loading = false;
      return false;
    }
    return true;
  }

  private performRegistration(): void {  
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
        Swal.fire({
          title: 'Registration successful!',
          text: 'Please check your email to confirm your account.',
          icon: 'success',
          confirmButtonText: 'OK',
          confirmButtonColor: '#5f99be',
        });
        this.loading = false;
        this.router.navigate(['/login']);
      },
      error: err => {
        let errorMsg = 'Registration failed!';
        if (err.error && typeof err.error === 'string') {
          errorMsg = err.error;
        } else if (err.error && err.error.message) {
          errorMsg = err.error.message;
        }
        Swal.fire({
          title: 'Registration Failed!',
          text: errorMsg,
          icon: 'error',
          confirmButtonText: 'OK',
          confirmButtonColor: '#df1b1b',
        });
        this.loading = false;
      }
    });
  } 
  
}
