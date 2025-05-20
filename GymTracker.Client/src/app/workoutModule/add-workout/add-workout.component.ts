import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import Swal from 'sweetalert2';
import { AuthService } from '../../authModule/auth.service';
import { ButtonComponent } from '../../sharedModule/button/button.component';
import { InputFieldComponent } from '../../sharedModule/input-field/input-field.component';
import { WorkoutService } from '../workout.service';
import { WorkoutSession } from '../workout.model';

@Component({
  selector: 'app-add-workout',
  standalone: true,
  imports: [CommonModule, FormsModule, InputFieldComponent, ButtonComponent, RouterModule],
  templateUrl: './add-workout.component.html',
  styleUrl: './add-workout.component.scss'
})
export class AddWorkoutComponent {

  timeStamp: string = '';
  type: number = 0;
  durationMinutes: string = '';
  caloriesBurned: string = '';
  intensity: number = 0;
  fatigueLevel: number = 0;
  notes = '';

  loading: boolean = false;

  constructor(private workoutService: WorkoutService, private router: Router) {}

  onSumbit() {
    if (!this.isValidInput()) {
      Swal.fire({
        title: 'Invalid input',
        text: 'Please fill in all required fields correctly.',
        icon: 'warning',
        confirmButtonColor: '#f0ad4e'
      });
      return;
    }
  
    this.loading = true;
    this.addWorkout();
  }

  private isValidInput(): boolean {
    if (!this.durationMinutes || isNaN(+this.durationMinutes)) {
      return false;
    }
    if (!this.caloriesBurned || isNaN(+this.caloriesBurned)) {
      return false;
    }
    if (this.intensity < 1 || this.intensity > 10) {
      return false;
    }
    if (this.fatigueLevel < 1 || this.fatigueLevel > 10) {
      return false;
    }
    if (!this.timeStamp || this.timeStamp.trim() === '') {
      return false;
    }
  
    return true;
  }

  private addWorkout(): void {  
    const workoutData: WorkoutSession = {
      timestamp: this.timeStamp,
      type: this.type,
      durationMinutes: +this.durationMinutes,
      caloriesBurned: +this.caloriesBurned,
      intensity: this.intensity,
      fatigueLevel: this.fatigueLevel,
      notes: this.notes
    };
  
    this.workoutService.createWorkout(workoutData).subscribe({
      next: () => {
        
      console.log("poziv");
        Swal.fire({
          title: 'Workout saved!',
          text: 'Your session has been recorded successfully.',
          icon: 'success',
          showConfirmButton: false,
          confirmButtonText: 'OK',
          confirmButtonColor: '#5f99be',
          timer: 3000,
          timerProgressBar: true,
          didClose: () => {
            this.router.navigate(['/workouts']);
          }
        });
        
        this.loading = false;
      },
      error: (err) => {
        Swal.fire({
          title: 'Error!',
          text: 'Failed to save workout session. Please try again.',
          icon: 'error',
          showConfirmButton: false,
          confirmButtonText: 'OK',
          confirmButtonColor: '#ff0f0f'
        });        
        this.loading = false;
      }
    });
  } 
}
