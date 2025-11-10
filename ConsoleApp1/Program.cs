using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CalendarRepository calendarRepository = new();
            string[] menu = { "1. Open Workout Manager", "2. Create Workout Calendar Event", "0. Exit Application" };
            Console.Clear();
            while (true)
            {
                Console.CursorVisible = false;
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
                        calendarRepository.Create();
                        break;

                    case 0:
                        System.Environment.Exit(0);
                        // if using winforms:   System.Windows.Forms.Application.Exit();
                        return;

                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid input. Please try again.\n");
                        break;
                }
            }
        }
    }
}