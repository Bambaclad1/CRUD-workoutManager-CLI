using System.ComponentModel.Design;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string[] menu = { "1. Open Workout Manager", "2. Check workout logbook", "0. Exit Application" };

            while (true)
            {
                Console.CursorVisible = false;
                Console.Clear();
                Console.Title = "FWT-CLI Menu Debug";
                Console.WriteLine("Raman's Fitness Workout Tracker CLI");

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
                        WorkoutManager.Menu();
                        break;

                    case 2:
                        break;

                    case 0:
                        System.Environment.Exit(0);
                        // if using winforms:   System.Windows.Forms.Application.Exit();
                        return;

                    default:
                        Console.WriteLine("Invalid input. Please try again.\n");
                        break;
                }
            }
        }
    }
}