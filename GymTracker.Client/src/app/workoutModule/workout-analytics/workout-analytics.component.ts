import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { WorkoutService } from '../workout.service';
import { WeeklyWorkoutDto } from '../workout.model';

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

  constructor(private workoutService: WorkoutService) {}

  ngOnInit(): void {
    this.loadData();
  }
  
  loadData(): void {
    const month = this.selectedMonth.getMonth() + 1;
    const year = this.selectedMonth.getFullYear();

    console.log("Loading data for:", month, year);

    this.workoutService.getMonthlySummary(month, year).subscribe({
      next: (data) => {
        console.log("Data loaded:", data);
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
  
  openWeekDetails(week: any) {
    alert(`Details for Week ${week.weekNumber}`);
  }
}
