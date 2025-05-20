import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { WorkoutService } from '../workout.service';
import { WeeklyWorkoutDto } from '../workout.model';
import { AuthService } from '../../authModule/auth.service';

@Component({
  selector: 'app-workout-analytics',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './workout-analytics.component.html',
  styleUrl: './workout-analytics.component.scss'
})
export class WorkoutAnalyticsComponent implements OnInit{
  selectedMonth = new Date();
  
  weeklyData: WeeklyWorkoutDto[] = [];
  popupVisible = false;

  constructor(private workoutService: WorkoutService, private router: Router, private authService: AuthService) {}

  ngOnInit(): void {
    this.loadData();
  }
  
  loadData(): void {
    const month = this.selectedMonth.getMonth() + 1;
    const year = this.selectedMonth.getFullYear();

    this.workoutService.getMonthlySummary(month, year).subscribe({
      next: (data) => {
        this.weeklyData = data;
      },
      error: (err) => {
        console.error('Failed to load workout data', err);
      }
    });
  }
  
  prevMonth() {
    this.selectedMonth = new Date(this.selectedMonth.setMonth(this.selectedMonth.getMonth() - 1));
    this.loadData();
  }
  
  nextMonth() {
    this.selectedMonth = new Date(this.selectedMonth.setMonth(this.selectedMonth.getMonth() + 1));
    this.loadData();
  }
  
  togglePopup() {
    this.popupVisible = !this.popupVisible;
  }

  closePopup(): void {
    this.popupVisible = false;
  }  

  logOut() {
    this.authService.logout();
    this.router.navigate(['/login']);
    this.popupVisible = false;
  }
}
