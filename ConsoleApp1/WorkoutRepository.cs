using System.Reflection;
using System.Text.Json;

namespace ConsoleApp1
{
    public class WorkoutRepository 
    {
        public void Menu()
        {
            ExcerciseRepository excerciseRepository = new ExcerciseRepository();
            Console.Clear();
            Console.WriteLine("Welcome to the Workout Repository Menu. Would you like to create a new Workout or add a Existing Workout (template?) For information about this works, please refer to the documentation, or press entry '3'");
            string[] menu = { "1. Create a new workout", "2. Add a existing workout (template) ", "3. Information about workouts", "0. Exit Menu" };

            while (true)
            {
                Console.WriteLine("Welcome to the workout manager");

                foreach (string items in menu)
                    Console.WriteLine(items);

                Console.WriteLine("\n");
                string feedback = Console.ReadLine();

                if (!int.TryParse(feedback, out int answer))
                {
                    Console.Clear();
                }

                switch (answer)
                {
                    case 1:
                        WorkoutOnboarding();
                        break;

                    case 2:
                        break;

                    case 3:
                        Console.WriteLine(Information());
                        break;

                    case 0:
                        return;

                    default:
                        Console.WriteLine("Invalid input. Please try again.\n");
                        break;
                }
            }
        }

        public string Information()
        {
            return """"
                One workout can have multiple exercises. If you don't see the excercise you like to add in your workout, you should add a excercise in the former menu.
                By creating a workout you can create a series of exercises, which is usually performed in a combination of one and another.
                The first step recommended is to create a series of exercises to be created in your workout. Then you can make a workout of it.

                """";
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
                        Saved excercise {count}.
                        Name: {ex.Name}
                        Description: {ex.Description}
                        """);

                }
                Console.WriteLine("______________________________\n\n");
                return (count, parsedExcercises);
            }
            catch (Exception e)
            {
                throw new FileNotFoundException("Error 404, while trying to access excerciseFormat, it was not found.", e);
            }
        }

        public List<int> AskUserInput(string printMe, int count)
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
                else
                {
                    userSelections.Add(output);
                    break;
                }
            }
            while (true)
            {
                Console.Write("Current excercies chosen: ");
                foreach (int useritem in userSelections)
                {
                    Console.Write(useritem + " ");
                }
                Console.WriteLine("\nWould you like to add more excercises in your workout? (y/n)");
                string answer = Console.ReadLine();

                if (String.IsNullOrEmpty(answer))
                {
                    Console.WriteLine("\n\nNo valid answer was given. Try again.");
                }

                switch (answer)
                {
                    case "y":
                        Console.WriteLine(printMe);
                        string input = Console.ReadLine();
                        if (!int.TryParse(input, out int output) || output > count || output <= 0)
                        {
                            Console.WriteLine("Not a valid answer! Try again please..");
                            continue;
                        }
                        else
                        {
                            userSelections.Add(output);
                            break;
                        }

                    case "n":
                        Console.WriteLine("Finished.");
                        return userSelections;
                }
            }
        }
        public void WorkoutOnboarding()
        {
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
                        Console.WriteLine(excerciseObject.excerciseCount);
                        AskUserInput("Current saved excercises have been displayed with a number. Please select a excercise to add in your Workout", excerciseObject.excerciseCount);
                        return;
                    case "n":
                        Console.Clear();
                        Console.WriteLine("Restarting then...");
                        continue;

                    default:
                        Console.WriteLine("\n\nNo valid answer was given (y/n). Try again.");
                        continue;
                }

            }
        }
    }
}