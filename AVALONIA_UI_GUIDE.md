# Running the Avalonia UI Application

## Prerequisites
- .NET 8.0 SDK or later
- A desktop environment (Windows, macOS, or Linux with X11/Wayland)

## How to Run

### Option 1: Using Visual Studio or Rider
1. Open `ConsoleApp1.sln` in Visual Studio or Rider
2. Set `WorkoutManagerUI` as the startup project
3. Press F5 or click the Run button

### Option 2: Using Command Line
```bash
cd WorkoutManagerUI
dotnet run
```

## Features

The Avalonia UI application provides a modern graphical interface for managing your workout exercises:

### Home Screen
- Welcome message and overview of features
- Quick access to all main functions

### Exercise List
- View all saved exercises
- See exercise details including:
  - Name and description
  - Difficulty level (1-3)
  - Sets and reps
  - Primary and secondary muscle groups
  - Equipment needed
  - Video demonstrations

### Create Exercise
- User-friendly form for creating new exercises
- Add multiple muscle groups (primary and secondary)
- Define execution steps
- Set workout configuration (sets, reps, rest time)
- Add equipment requirements
- Include video URL for demonstrations

## Navigation

The application uses a sidebar navigation menu with three main sections:

1. **🏠 Home** - Main dashboard
2. **💪 Exercises** - View all exercises
3. **➕ Create Exercise** - Create new exercises

## Data Storage

Exercises are saved in JSON format in the `excerciseFormat.json` file, which is shared with the console application. This allows you to use both interfaces interchangeably.

## Architecture

The application follows the MVVM (Model-View-ViewModel) pattern:

- **Models**: Shared with ConsoleApp1 (Excercise, ExcerciseRepository)
- **ViewModels**: Handle business logic and data binding
  - `MainWindowViewModel`: Main navigation and orchestration
  - `HomeViewModel`: Home screen logic
  - `ExercisesViewModel`: Exercise list logic
  - `CreateExerciseViewModel`: Exercise creation logic
- **Views**: XAML-based UI components
  - `MainWindow`: Main application window with navigation
  - `HomeView`: Welcome screen
  - `ExercisesView`: Exercise list display
  - `CreateExerciseView`: Exercise creation form

## Styling

The application uses Avalonia's Fluent theme with custom colors:
- Sidebar: Dark theme (#2C3E50)
- Main content area: Light background (#ECF0F1)
- Exercise cards: White with subtle shadows
- Accent colors for different element types
