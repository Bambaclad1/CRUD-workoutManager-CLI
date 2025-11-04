using System.Reflection;
using System.Text.Json;
using System.Collections;
using System.Runtime.CompilerServices;

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
                Console.WriteLine("______________________________\n\n");
                return (count, parsedExcercises);
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