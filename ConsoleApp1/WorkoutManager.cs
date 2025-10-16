using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class WorkoutManager
    {
        public static void Menu()
        {
            ExerciseCreator exerciseCreator = new();
            ExcerciseRepository excerciseRepository = new();
            WorkoutRepository workoutRepository = new();

            string[] menu = { "1. Create a excercise", "2. Read a excercise ", "3. Create/Add a workout", "4. Edit a exercise", "5. Edit a workout", "0. Exit Application" };

            Console.Clear();
            while (true)
            {
                Console.Title = "FWT-CLI WorkoutManager";
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
                        ExcerciseRepository.SaveExcercises(exerciseCreator.Create());
                        break;

                    case 2:
                        ExcerciseRepository.Read();
                        break;

                    case 3:
                        workoutRepository.Menu();
                        break;

                    case 0:
                        return;

                    default:
                        Console.WriteLine("Invalid input. Please try again.\n");
                        break;
                }
            }
        }
    }
}