# Avalonia UI Application Structure

## Visual Layout

```
┌────────────────────────────────────────────────────────────────────────┐
│ Workout Manager                                                         │
├─────────────┬──────────────────────────────────────────────────────────┤
│             │                                                            │
│  Sidebar    │              Main Content Area                            │
│  (200px)    │              (Dynamic Views)                              │
│             │                                                            │
│ ┌─────────┐ │                                                            │
│ │ Workout │ │  ┌──────────────────────────────────────────────────┐   │
│ │ Manager │ │  │                                                  │   │
│ └─────────┘ │  │         Content changes based on selected        │   │
│             │  │         navigation item:                         │   │
│ ┌─────────┐ │  │                                                  │   │
│ │🏠 Home  │ │  │   - Home: Welcome screen & features              │   │
│ └─────────┘ │  │   - Exercises: List of all exercises             │   │
│             │  │   - Create: Exercise creation form               │   │
│ ┌─────────┐ │  │                                                  │   │
│ │💪 Exer- │ │  │                                                  │   │
│ │  cises  │ │  │                                                  │   │
│ └─────────┘ │  │                                                  │   │
│             │  │                                                  │   │
│ ┌─────────┐ │  │                                                  │   │
│ │➕ Create│ │  │                                                  │   │
│ │ Exercise│ │  │                                                  │   │
│ └─────────┘ │  └──────────────────────────────────────────────────┘   │
│             │                                                            │
│             │                                                            │
└─────────────┴──────────────────────────────────────────────────────────┘
```

## View Details

### 1. Home View
```
┌────────────────────────────────────────────────────────┐
│  Welcome to Workout Manager                            │
│  Manage your exercises and workouts with ease          │
│                                                        │
│  ┌──────────────────────────────────────────────────┐ │
│  │ Features:                                        │ │
│  │ ✓ Create and manage exercises                   │ │
│  │ ✓ View all your exercises                       │ │
│  │ ✓ Track sets, reps, and rest times              │ │
│  │ ✓ Add detailed execution steps                  │ │
│  │ ✓ Include video URLs for demonstrations         │ │
│  └──────────────────────────────────────────────────┘ │
└────────────────────────────────────────────────────────┘
```

### 2. Exercises View
```
┌────────────────────────────────────────────────────────┐
│  My Exercises                                          │
│                                                        │
│  ┌──────────────────────────────────────────────────┐ │
│  │ Bench Press                                      │ │
│  │ Compound chest exercise                          │ │
│  │ Difficulty: 2  Sets: 4  Reps: 10                 │ │
│  └──────────────────────────────────────────────────┘ │
│                                                        │
│  ┌──────────────────────────────────────────────────┐ │
│  │ Pull-ups                                         │ │
│  │ Upper back and biceps exercise                   │ │
│  │ Difficulty: 3  Sets: 3  Reps: 8                  │ │
│  └──────────────────────────────────────────────────┘ │
│                                                        │
│  [Scrollable list of all exercises...]                │
└────────────────────────────────────────────────────────┘
```

### 3. Create Exercise View
```
┌────────────────────────────────────────────────────────┐
│  Create New Exercise                                   │
│                                                        │
│  Basic Information                                     │
│  ┌──────────────────────────────────────────────────┐ │
│  │ Exercise Name: [___________________]             │ │
│  │ Description:  [___________________]              │ │
│  │ Difficulty:   [1-3 selector]                     │ │
│  └──────────────────────────────────────────────────┘ │
│                                                        │
│  Muscle Groups                                         │
│  ┌──────────────────────────────────────────────────┐ │
│  │ Primary Focus:   [_________] [Add]               │ │
│  │ Tags: [Chest] [Back]                             │ │
│  │                                                  │ │
│  │ Secondary Focus: [_________] [Add]               │ │
│  │ Tags: [Triceps] [Shoulders]                      │ │
│  └──────────────────────────────────────────────────┘ │
│                                                        │
│  Execution Steps                                       │
│  ┌──────────────────────────────────────────────────┐ │
│  │ [__________________] [Add Step]                  │ │
│  │ Step 1. Lie on bench...        [Remove]          │ │
│  │ Step 2. Grip the bar...         [Remove]          │ │
│  └──────────────────────────────────────────────────┘ │
│                                                        │
│  Workout Configuration                                 │
│  ┌──────────────────────────────────────────────────┐ │
│  │ Sets: [3]  Reps: [12]  Rest Time: [60] seconds  │ │
│  └──────────────────────────────────────────────────┘ │
│                                                        │
│  Equipment                                             │
│  ┌──────────────────────────────────────────────────┐ │
│  │ [__________________] [Add Equipment]             │ │
│  │ [Barbell] [Bench]                                │ │
│  └──────────────────────────────────────────────────┘ │
│                                                        │
│  [Cancel]                    [Save Exercise]           │
└────────────────────────────────────────────────────────┘
```

## Color Scheme

- **Sidebar Background**: #2C3E50 (Dark blue-gray)
- **Main Content Area**: #ECF0F1 (Light gray)
- **Exercise Cards**: White with subtle shadows
- **Primary Tags**: #3498DB (Blue)
- **Secondary Tags**: #2ECC71 (Green)
- **Equipment Tags**: #E67E22 (Orange)
- **Save Button**: #27AE60 (Green)

## Data Flow

```
User Interaction
       ↓
   ViewModel (Commands)
       ↓
Business Logic (ConsoleApp1)
       ↓
ExerciseRepository
       ↓
JSON File (excerciseFormat.json)
       ↓
Observable Collection
       ↓
View (Data Binding)
       ↓
UI Display
```

## Key Components

1. **ViewLocator**: Automatically resolves Views from ViewModels
2. **Command Pattern**: All buttons use ICommand interface
3. **Observable Properties**: Using CommunityToolkit.Mvvm attributes
4. **Data Templates**: XAML templates for different data types
5. **Shared Models**: Reuses Excercise class from ConsoleApp1
