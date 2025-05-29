import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { WeeklyWorkoutDto, WorkoutSession } from './workout.model';
import { environment } from '../../environment';

@Injectable({
  providedIn: 'root'
})
export class WorkoutService {
  private apiUrl = `${environment.apiBaseUrl}/workouts`;

  constructor(private http: HttpClient) {}

  getMonthlySummary(month: number, year: number): Observable<WeeklyWorkoutDto[]> {
    return this.http.get<WeeklyWorkoutDto[]>(`${this.apiUrl}/monthly-summary?month=${month}&year=${year}`);
  }

  createWorkout(data: WorkoutSession): Observable<any> {
    return this.http.post(this.apiUrl, data);
  }  
}
