using ConsoleApp1.util;
using System;
using System.Collections;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Transactions;

namespace ConsoleApp1
{
    public class WorkoutRepository
    {
        private static readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true,
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true
        };

        public static void Read()
        {
            try
            {
                string jsonpath = AppDomain.CurrentDomain.BaseDirectory + "\\workoutFormat.json";
                string content = File.ReadAllText(jsonpath);
                List<Workout> parsedWorkouts = JsonSerializer.Deserialize<List<Workout>>(content);

                foreach (var ex in parsedWorkouts)
                {
                    UseReflection(ex);
                }
                return;
            }
            catch (Exception e)
            {
                throw new FileNotFoundException("Error 404, while trying to access excerciseFormat, it was not found.", e);
            }
        }

        public void RemoveWorkout()
        {
            while (true)
            {
                Console.WriteLine("Would you like to remove a workout or all workouts?");
                Console.Write("(One/All): ");
                string answer = Console.ReadLine();

                if (answer.Equals("One", StringComparison.OrdinalIgnoreCase))
                {
                    bool success = RemoveOneWorkout();
                    if (!success)
                        throw new Exception("Unable to wipe ExerciseRepository?! Does it exist??");
                    Console.WriteLine("\nSuccess!");
                    return;
                }
                else if (answer.Equals("All", StringComparison.OrdinalIgnoreCase))
                {
                    bool success = WipeWorkoutRepository();
                    if (!success)
                        throw new Exception("Unable to wipe ExerciseRepository?! Does it exist??");
                    Console.WriteLine("\nSuccess!");
                    return;
                }
                else continue;
            }
        }

        public bool RemoveOneWorkout()
        {
            try
            {
                List<int> count;
                int index;

                var workoutObject = ReadWorkouts();
                count = AskUserInput("Workouts and numbers have been printen down. Please select a workout to modify.", workoutObject.count, true);

                index = count[0] - 1;

                JsonNode jsonnode = JsonNode.Parse(ReturnJsonRepository());
                jsonnode.AsArray().RemoveAt(index);

                List<Workout>? exercises = jsonnode.Deserialize<List<Workout>>(_options);

                bool isWiped = WipeWorkoutRepository();
                if (!isWiped)
                    throw new FileNotFoundException("Something went wrong, workoutFormat was not wiped because this was not found!");

                SaveWorkoutsList(exercises);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error in TryCatch Block! Error: ", e);
            }
        }
        public static void SaveExcercises(Workout buildedWorkout)
        {
            Console.WriteLine("Saving workout, please wait...");
            try
            {
                string jsonPath = AppDomain.CurrentDomain.BaseDirectory + "\\workoutFormat.json";

                List<Workout> workouts = new();

                if (File.Exists(jsonPath))
                {
                    string existingContent = File.ReadAllText(jsonPath);

                    if (!string.IsNullOrWhiteSpace(existingContent))
                    {
                        workouts = JsonSerializer.Deserialize<List<Workout>>(existingContent, _options)
                                    ?? new List<Workout>();
                    }
                }

                workouts.Add(buildedWorkout);

                string jsonReady = JsonSerializer.Serialize(workouts, _options);

                File.WriteAllText(jsonPath, jsonReady);

                Console.WriteLine("Saved workout successfully!");
            }
            catch (Exception e)
            {
                throw new Exception("Unknown exception, failed to save workout.  ", e);
            }
        }

        public (int excerciseCount, List<Excercise> parsedExcercises) ReadExercises()
        {
            try
            {
                string jsonpath = AppDomain.CurrentDomain.BaseDirectory + "\\excerciseFormat.json";
                string content = File.ReadAllText(jsonpath);
                List<Excercise> parsedExcercises = JsonSerializer.Deserialize<List<Excercise>>(content);
                int count = 0;
                foreach (var ex in parsedExcercises)
                {
                    count++;
                    Console.WriteLine($"""
                        ______________________________
                        Exercise {count}. {ex.Name}
                        Description: {ex.Description}
                        """);
                }
                Console.WriteLine("______________________________");
                return (count, parsedExcercises);
            }
            catch (Exception e)
            {
                Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
                Console.WriteLine("Warning: No excerciseFormat.json file found. Have you tried creating a exercise first? (BE WARY OF TYPO!)");
                throw new FileNotFoundException("Error 404, while trying to access excerciseFormat, it was not found.", e);
            }
        }

        public (int count, List<Workout> parsedWorkouts) ReadWorkouts()
        {
            try
            {
                string jsonpath = AppDomain.CurrentDomain.BaseDirectory + "\\workoutFormat.json";
                string content = File.ReadAllText(jsonpath);
                List<Workout> parsedWorkouts = JsonSerializer.Deserialize<List<Workout>>(content);
                int count = 0;
                foreach (var ex in parsedWorkouts)
                {
                    count++;
                    Console.WriteLine($"""
                        ______________________________
                        Workout {count}. {ex.Name}
                        Description: {ex.Description}
                        Exercices Count: {ex.Excercises.Count}
                        """);
                }
                Console.WriteLine("______________________________");
                return (count, parsedWorkouts);
            }
            catch (Exception e)
            {
                Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
                Console.WriteLine("Warning: No excerciseFormat.json file found. Have you tried creating a exercise first? (BE WARY OF TYPO!)");
                throw new FileNotFoundException("Error 404, while trying to access excerciseFormat, it was not found.", e);
            }
        }
        public List<int> AskUserInput(string printMe, int count, bool AskOnce)
        {
            List<int> userSelections = new();

            while (true)
            {
                Console.WriteLine(printMe);
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int output) || output > count || output <= 0)
                {
                    Console.WriteLine("Not a valid answer! Try again please..");
                    continue;
                }

                userSelections.Add(output);

                if (AskOnce)
                    return userSelections;
                // --- Ask if the user wants to continue ---
                while (true)
                {
                    Console.WriteLine("\nWould you like to add more exercises to your workout? (y/n)");
                    string answer = Console.ReadLine()?.Trim().ToLower();

                    if (string.IsNullOrEmpty(answer))
                    {
                        Console.WriteLine("No valid answer was given. Try again.");
                        continue;
                    }

                    if (answer == "y")
                    {
                        break;
                    }
                    else if (answer == "n")
                    {
                        Console.WriteLine("Finished.\n");
                        return userSelections;
                    }
                    else
                    {
                        Console.WriteLine("Please type 'y' or 'n'.");
                    }
                }
            }
        }

        public List<Excercise> ReturnSelectedExcercises(List<Excercise> parsedExcercises, List<int> userSelections)
        {
            List<Excercise> selectedExercises = new();
            Console.WriteLine("\n\n\nCurrent chosen exercises: ");
            foreach (int number in userSelections)
            {
                int index = number - 1;
                var ex = parsedExcercises[index];
                selectedExercises.Add(ex);
                Console.WriteLine($"\n{number}. Name: {ex.Name} \nDescription: {ex.Description}\n");
            }
            return selectedExercises;
        }

        public void Edit()
        {
            Workout editedWorkout = new();
            Workout newWorkout = new();
            List<int> count;
            int index;

            var workoutObject = ReadWorkouts();
            count = AskUserInput("Workouts and numbers have been printen down. Please select a workout to modify.", workoutObject.count, true);

            index = count[0] - 1;

            Console.Write("\nWould you like to view all current workout information? (y/n):");
            char yn = Console.ReadKey().KeyChar;

            if (char.ToLowerInvariant(yn) == 'y')
            {
                var print = workoutObject.parsedWorkouts[index];
                Console.WriteLine($"""
                    ___ 
                    Current workout information:
                    Name: {print.Name}
                    Description: {print.Name}
                    Exercises Count: {print.Excercises.Count}
                    Excercise One First Name: {print.Excercises[1].Name}
                    """);
            }
            Console.WriteLine("\nPress any key to enter the edit menu...");
            Console.ReadKey();

            newWorkout = EditMenu(workoutObject.parsedWorkouts[index]);

            workoutObject.parsedWorkouts[index] = newWorkout;

            Console.WriteLine(workoutObject.parsedWorkouts[index].Name);
            JsonNode jsonnode = JsonNode.Parse(ReturnJsonRepository());
            jsonnode.AsArray().RemoveAt(index);

            jsonnode.AsArray().Add(newWorkout);
            List<Workout>? workouts = jsonnode.Deserialize<List<Workout>>(_options);

            bool isWiped = WipeWorkoutRepository();
            if (!isWiped)
                throw new FileNotFoundException("Something went wrong, Exerciseformat was not wiped because this was not found!");

            SaveWorkoutsList(workouts);
            Console.WriteLine("\nFinished!");
        }

        public bool WipeWorkoutRepository()
        {
            string jsonPath = AppDomain.CurrentDomain.BaseDirectory + "\\workoutFormat.json";

            if (File.Exists(jsonPath))
            {
                File.Delete(jsonPath);
                return true;
            }
            else
            {
                Console.WriteLine("exerciseFormat was not found! Not able to delete.");
                return false;
            }
        }

        public string ReturnJsonRepository()
        {
            try
            {
                string jsonpath = AppDomain.CurrentDomain.BaseDirectory + "\\workoutFormat.json";
                string content = File.ReadAllText(jsonpath);
                List<Workout> parsedExcercises = JsonSerializer.Deserialize<List<Workout>>(content);

                return content;
            }
            catch (Exception e)
            {
                throw new FileNotFoundException("Error 404, while trying to access excerciseFormat, it was not found.", e);
            }
        }

        public static void SaveWorkoutsList(List<Workout> w)
        {
            try
            {
                string jsonPath = AppDomain.CurrentDomain.BaseDirectory + "\\workoutFormat.json";

                if (File.Exists(jsonPath))
                {
                    string existingContent = File.ReadAllText(jsonPath);

                    if (!string.IsNullOrWhiteSpace(existingContent))
                    {
                        w = JsonSerializer.Deserialize<List<Workout>>(existingContent, _options)
                                    ?? new List<Workout>();
                    }
                }

                string jsonReady = JsonSerializer.Serialize(w, _options);

                File.WriteAllText(jsonPath, jsonReady);

                Console.WriteLine("Saved workout successfully!");
            }
            catch (Exception e)
            {
                throw new Exception("Unknown exception, failed to save workout.  ", e);
            }
        }

        public Workout EditMenu(Workout w)
        {
            List<int> count;
            Workout oldW = (Workout)w.Clone();
            List<Excercise> selectedExercises = new();

            while (true)
            {
                Console.WriteLine("\n");
                Console.WriteLine($"""
                    ----------
                    Workout Edit Menu for {w.Name}
                    ----------
                    1. Change Name
                    2. Change Description
                    3. Change Exercises

                    4. See differences between original and modified workout
                    5. Revert changes to original workout
                    0. End editing (Save)

                    """);
                string input = Console.ReadLine();
                int output = CheckIfValidIntFromString.Check(input, 0, 5);

                switch (output)
                {
                    case 1:
                        Console.WriteLine("Please enter the name for your workout: ");
                        w.Name = Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("Now enter the description: ");
                        w.Description =  Console.ReadLine();
                        break;
                    case 3:
                        var excerciseObject = ReadExercises();
                        count = AskUserInput("Current saved excercises have been displayed with a number. Please select a excercise to add in your Workout", excerciseObject.excerciseCount, false);
                        selectedExercises = ReturnSelectedExcercises(excerciseObject.parsedExcercises, count);
                        foreach (Excercise ex in selectedExercises)
                        {
                            w.AddExcercise(ex);
                        }
                        break;
                    case 4:
                        Console.WriteLine("\n\nOld Workout:");
                        UseReflection(oldW);

                        Console.WriteLine("\n\nNew Workout:");
                        UseReflection(w);

                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                    case 5:
                        w = oldW;
                        Console.WriteLine("\nSuccessfully done!");
                        break;
                    case 0:
                        return w;
                    case 409:
                        continue;
                }
            }
        }

        public Workout WorkoutOnboarding()
        {
            // get user data
            List<int> count;
            List<Excercise> selectedExercises = new();
            Workout workout = new();
            while (true)
            {
                Console.WriteLine("You are now in a menu to create a workout. Please enter the name for your workout: ");
                string a = Console.ReadLine();

                Console.WriteLine("Now enter the description: ");
                string b = Console.ReadLine();
                Console.WriteLine($"\nRight now your workout looks like this, name: {a} | description: {b}");
                Console.WriteLine("Does that look good to you? (y/n):");

                string userinput = Console.ReadLine();

                if (String.IsNullOrEmpty(userinput))
                {
                    Console.WriteLine("\n\nNo valid answer was given. Try again.");
                }

                switch (userinput)
                {
                    case "y":
                        var excerciseObject = ReadExercises();
                        count = AskUserInput("Current saved excercises have been displayed with a number. Please select a excercise to add in your Workout", excerciseObject.excerciseCount, false);
                        selectedExercises = ReturnSelectedExcercises(excerciseObject.parsedExcercises, count);
                        break;

                    case "n":
                        Console.Clear();
                        Console.WriteLine("Restarting then...");
                        continue;

                    default:
                        Console.WriteLine("\n\nNo valid answer was given (y/n). Try again.");
                        continue;
                }
                workout.Name = a;
                workout.Description = b;
                foreach (Excercise ex in selectedExercises)
                {
                    workout.AddExcercise(ex);
                }
                Console.WriteLine("\nYour workout is successfully created.\n");

                return workout;
            }
        }

        public static void UseReflection(Workout buildedWorkout)
        {
            PropertyInfo[] properties = typeof(Workout).GetProperties();
            Console.WriteLine("\n");
            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(buildedWorkout);
                string name = property.Name;

                if (value is IEnumerable enumerable && value is not string)
                {
                    string joined = string.Join(", ", enumerable.Cast<object>());
                    Console.WriteLine($"{name} = {joined}");
                }
                else
                    Console.WriteLine($"{name} = {value}");
            }
            Console.WriteLine("\n");
        }
    }
}