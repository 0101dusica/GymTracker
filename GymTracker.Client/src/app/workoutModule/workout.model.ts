export enum ExerciseType {
    Cardio = 0,
    Strength = 1,
    Flexibility = 2,
    Other = 3
}

export interface WorkoutSession {
    id: string;
    timestamp: string;
    type: ExerciseType;
    durationMinutes: number;
    caloriesBurned: number;
    intensity: number;       
    fatigueLevel: number;  
    notes?: string;
    userId: string;
}
  