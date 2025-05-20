import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { WeeklyWorkoutDto } from './workout.model';

@Injectable({
  providedIn: 'root'
})
export class WorkoutService {
  private apiUrl = 'http://localhost:5000/api/workouts';

  constructor(private http: HttpClient) {}

  getMonthlySummary(month: number, year: number): Observable<WeeklyWorkoutDto[]> {
    return this.http.get<WeeklyWorkoutDto[]>(`${this.apiUrl}/monthly-summary?month=${month}&year=${year}`);
  }
}
