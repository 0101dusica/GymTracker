import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-workout-analytics',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './workout-analytics.component.html',
  styleUrl: './workout-analytics.component.scss'
})
export class WorkoutAnalyticsComponent {
  selectedMonth = new Date();

  weeklyData = [
    { dates: "28.04. - 04.05.", totalDuration: 180, workoutCount: 3, avgIntensity: 6, avgFatigue: 5 },
    { dates: "05.05. - 11.05.", totalDuration: 240, workoutCount: 4, avgIntensity: 7, avgFatigue: 6 },
    { dates: "12.05. - 18.05.", totalDuration: 120, workoutCount: 2, avgIntensity: 5, avgFatigue: 4 },
    { dates: "19.05. - 25.05.", totalDuration: 300, workoutCount: 5, avgIntensity: 8, avgFatigue: 7 },
    { dates: "26.05. - 01.06.", totalDuration: 230, workoutCount: 4, avgIntensity: 6, avgFatigue: 5 },
  ];
  
  prevMonth() {
    this.selectedMonth = new Date(this.selectedMonth.setMonth(this.selectedMonth.getMonth() - 1));
  }
  
  nextMonth() {
    this.selectedMonth = new Date(this.selectedMonth.setMonth(this.selectedMonth.getMonth() + 1));
  }
  
  openWeekDetails(week: any) {
    alert(`Details for Week ${week.weekNumber}`);
  }
}
