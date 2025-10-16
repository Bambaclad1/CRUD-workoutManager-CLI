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

        public void ReadExercises()
        {
            try
            {
                string jsonpath = AppDomain.CurrentDomain.BaseDirectory + "\\excerciseFormat.json";
                string content = File.ReadAllText(jsonpath);
                List<Excercise> parsedExcercises = JsonSerializer.Deserialize<List<Excercise>>(content);
                int count = 1;
                foreach (var ex in parsedExcercises)
                {
                    Console.WriteLine($"""
                        Saved excercise {count} 
                        Name: {ex.Name}
                        Description: {ex.Description}
                        """);
                    count++;
                }

                return;
            }
            catch (Exception e)
            {
                throw new FileNotFoundException("Error 404, while trying to access excerciseFormat, it was not found.", e);
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
            Console.WriteLine("Next part");

        }
    }
}