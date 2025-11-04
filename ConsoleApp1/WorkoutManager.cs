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

            string[] menu = { "1. Create a excercise", "2. Read a excercise ", "3. Create a workout", "4. Read a workout", "5. Edit a exercise", "6. Edit a workout", "7. Remove a exercise", "8. Remove a workout", "0. Exit Application" };

            Console.Clear();
            while (true)
            {
                Console.Title = "FWT-CLI WorkoutManager";
                Console.WriteLine("\nWelcome to the workout manager");

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
                        WorkoutRepository.SaveExcercises(workoutRepository.WorkoutOnboarding());
                        break;

                    case 4:
                        WorkoutRepository.Read();
                        break;
                    case 5:
                       excerciseRepository.Edit();
                        break;
                    case 6:
                        return;
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