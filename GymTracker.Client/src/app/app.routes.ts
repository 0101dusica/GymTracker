import { Routes } from '@angular/router';
import { LoginComponent } from "./authModule/login/login.component";
import { RegisterComponent } from "./authModule/register/register.component";
import { UserDetailComponent } from "./userModule/user-detail/user-detail.component";
import { AddWorkoutComponent } from "./workoutModule/add-workout/add-workout.component";
import { WorkoutAnalyticsComponent } from "./workoutModule/workout-analytics/workout-analytics.component";
import { authGuard } from './authModule/guards/auth.guard';
import { authRequiredGuard } from './authModule/guards/auth-required-guard.guard';

export const routes: Routes = [
    { path: 'login', component: LoginComponent, canActivate: [authGuard] },
    { path: 'register', component: RegisterComponent, canActivate: [authGuard] },
    { path: 'users/:id', component: UserDetailComponent, canActivate: [authRequiredGuard] },
    { path: 'workouts', component: WorkoutAnalyticsComponent, canActivate: [authRequiredGuard] },
    { path: 'workouts/create', component: AddWorkoutComponent, canActivate: [authRequiredGuard] },
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    { path: '**', redirectTo: '/login' }
];  
