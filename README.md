# GymTracker

GymTracker is a web application designed to help users track their workout progress effectively. It provides login and registration functionality secured with JWT tokens. Users who don't have an account can register through the Register tab. However, login is only possible after email confirmation.

## Features

- **User Authentication:** Login and registration secured via JWT tokens.
- **Email Confirmation:** Users must confirm their email before they can log in.
- **Dashboard:** Displays workout data for the current month, including weekly workout counts, total workout duration, and average fatigue and intensity values.
- **Workout Management:** Users can add new workout sessions which immediately appear on the dashboard.
- **User Profile:** Users can update their personal data at any time except for their email and password.
- **Logout:** Secure logout functionality.
- **Session Persistence:** JWT tokens are valid for 24 hours, allowing users to stay logged in without re-authenticating during that period.

## Technical Overview

### Frontend (`GymTracker.Client`)

- Built with Angular.
- Handles all user interactions, including authentication, workout entry, and profile management.
- Consumes API endpoints provided by the backend.

### Backend (`GymTracker` solution)

The backend is structured in a modular way for maintainability and scalability:

- **Core:** Contains the main entities/models such as `User` and `WorkoutSession`.
- **Infrastructure:** Implements database access and related infrastructure logic.
- **Application:** Contains business logic, DTOs, interfaces, services, and mapping profiles.
- **API:** Main application layer with controllers and program entry point.

The backend is implemented using C# and .NET.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version compatible with the project)
- [Node.js](https://nodejs.org/) (for Angular frontend)
- Angular CLI (`npm install -g @angular/cli`)

### Running the Backend

1. Navigate to the API project folder (usually `GymTracker.Api`).
2. Run the following command to start the backend server:

   ```bash
   dotnet run


3. The API will start and listen on the configured port (usually http://localhost:5000).

## Running the Frontend

1. Navigate to the Angular client folder (`GymTracker.Client`).

2. Install dependencies:

   ```bash
   npm install

3. Start the Angular development server:

   ```bash
   ng serve

4. Open your browser and go to http://localhost:4200.

## Notes

- **Backend First:** Make sure the backend is running before starting the frontend to avoid API connection issues.
- **JWT Token Expiration:** The JWT token is valid for 24 hours. After expiration, the user must log in again.
- **Email Confirmation Required:** Users must confirm their email address before they are allowed to log in.
- **Immutable Credentials:** User email and password cannot be changed through the profile settings.