import { Routes } from "@angular/router";
import { LoginComponent } from "./authModule/login/login.component";
import { RegisterComponent } from "./authModule/register/register.component";
import { UserDetailComponent } from "./userModule/user-detail/user-detail.component";
import { AddWorkoutComponent } from "./workoutModule/add-workout/add-workout.component";
import { WorkoutAnalyticsComponent } from "./workoutModule/workout-analytics/workout-analytics.component";

const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'users/:id', component: UserDetailComponent },
    { path: 'workouts', component: WorkoutAnalyticsComponent},
    { path: 'workouts/create', component: AddWorkoutComponent },
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    { path: '**', redirectTo: '/login' }
];  