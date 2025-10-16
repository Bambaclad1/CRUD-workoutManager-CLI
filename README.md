# Just a CRUD Workout Manager
CRUD DotNet application allowing the user to perform CRUD operations with a application. This project is split in two phases:

- [x] Phase 1: Create a CRUD application as a console application in C#
- [x] Phase 2: Integrate Avalonia UI Framework for a graphical application

So what does it do?
As a user, you can perform the following actions:
* Create a excercise through a guided step-for-step onboarding (Console) or intuitive UI form (Avalonia)
* Retrieve excercises created saved in a .json format
* Create a workout, which can contain multiple excercises
* Retrieve a workout, saved in a .json as well
* Update excercises/workouts
* Remove excercises/workouts

## How to Use

### Console Application
```bash
cd ConsoleApp1
dotnet run
```

### Avalonia UI Application (NEW!)
```bash
cd WorkoutManagerUI
dotnet run
```

For detailed instructions on using the UI, see [AVALONIA_UI_GUIDE.md](AVALONIA_UI_GUIDE.md)

## Project Structure

The solution now contains two projects:

1. **ConsoleApp1** - Original console-based application with CRUD functionality
2. **WorkoutManagerUI** - Modern Avalonia UI frontend with MVVM architecture

Both projects share the same data models and JSON storage, allowing seamless interoperability.

## Contents
* (personal) To-Do List
* Tech Stack
* Implementing Avalonia UI into the console app
* Create Opreations
	* Builder Pattern
	* The not so convenient solution
	* Saving data into JSON
* Read Operations
	* Saving excercise(s) into a JSON file
	* The Workouts to Excercise relationship
	* How did I approach this problem?
* Update Operations
* Remove Operations
* Implementing Avalonia UI into the console app
* Roadblocks I went through and learned from
* Disclaimer

## (personal) To-Do List
### Phase One
**'Backend' implementation**
- [x] Project initialization
- [x] Create A Excercise
- [x] Implement Builder Pattern at Create
- [x] Read Operation
- [x] Get Writing/Reading .json files working
- [ ] Create Workout. One workout = multiple excercises
- [ ] Read Workout. Same .json method
- [ ] Create a Calendar (Use a package..?) and add the possibility to add workouts to it
- [ ] ICal Intregration
- [ ] Edit existing excercies (use the same builder pattern?)
- [ ] Edit existing workouts
- [ ] Remove Excercises/Workouts

### Phase Two
**'Frontend' inplementation**
- [x] Get familiar with Avalonia UI
- [x] Do some Research on tool dashboard UI's. Find websites that offer UI examples
- [x] Create a Figma Design for the frontend website. (Use Avalonia UI examples for the best success given time).
- [x] Create the Front-End
- [x] Design a dashboard-like interface.

## Implementing Avalonia UI into the console app

The Avalonia UI implementation brings a modern, cross-platform graphical interface to the Workout Manager application. Here's what was implemented:

### Architecture
- **MVVM Pattern**: Clean separation of concerns with ViewModels, Views, and Models
- **Shared Business Logic**: The UI project references ConsoleApp1, reusing all existing models and repositories
- **ViewLocator Pattern**: Automatic view resolution based on ViewModel names

### UI Components
1. **MainWindow**: Container with sidebar navigation and dynamic content area
2. **HomeView**: Welcome screen with feature overview
3. **ExercisesView**: List view displaying all exercises with their details
4. **CreateExerciseView**: Comprehensive form for creating new exercises with:
   - Basic information (name, description, difficulty)
   - Muscle group targeting (primary and secondary)
   - Detailed exercise information
   - Step-by-step execution instructions
   - Workout configuration (sets, reps, rest time)
   - Equipment requirements
   - Video URL support

### Key Features
- **Responsive Layout**: Grid-based layout with sidebar navigation
- **Modern Design**: Uses Avalonia's Fluent theme with custom color scheme
- **Data Binding**: Two-way data binding for all form fields
- **Command Pattern**: All actions use ICommand for better testability
- **Observable Collections**: Reactive UI updates when data changes

### Technology Stack
- Avalonia UI 11.3.6
- CommunityToolkit.Mvvm 8.2.1 for MVVM helpers
- .NET 8.0
- Shared JSON data storage with console app

## Tech Stack
Do people these days actually share their entire stack developing programs? I think it's nice to have in a documentation, just to let others know how I coded this program.

## Disclaimer
**AI Usage Disclaimer**: *AI was mostly used for help with complex parts such as serializing JSON, aside from that, youtube videos,
and Microsoft docs/GeeksForGeeks/W3Schools was used. Oh btw, ChatGPT created this cool bannner at the start of the page :)*

nvm this shit is terrible
<img width="855" height="543" alt="image" src="https://github.com/user-attachments/assets/baa28a92-8246-4e2f-a014-04ae378fb05d" />
