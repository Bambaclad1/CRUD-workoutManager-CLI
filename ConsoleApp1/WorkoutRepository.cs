using System.Reflection;

namespace ConsoleApp1
{
    public interface IBuilder2
    {
        void AskName();
    }

    public class WorkoutRepository : IBuilder2
    {
        public void Menu()
        {
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
                        Create();
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
                One workout can have multiple excercises. If you don't see the excercise you like to add in your workout, you should add a excercise in the former menu.
                By creating a workout you can create a series of excercises, which is usually performed in a combination of one and another.
                idk what else

                """";
        }

        private Excercise _excercise = new Excercise();

        public WorkoutRepository()
        {
            this.Reset();
        }

        public void Reset()
        {
            this._excercise = new Excercise();
        }

        public void AskName()
        {
            Console.Write("\nEnter your Excercise Name: ");
            _excercise.Name = Console.ReadLine();
        }

        public Excercise GetProduct()
        {
            Excercise result = this._excercise;

            this.Reset();

            return result;
        }

        public Excercise Create()
        {
            Console.Title = "FWT-CLI ExcerciseCreator";

            // Implemented Builder Pattern from Refactoring.Guru

            var builder = new ExcerciseBuilder();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("This is the menu for creating a excercise.");

                builder.AskName();

                Console.WriteLine("\n\n");
                Excercise buildedExcercise = builder.GetProduct();

                // Uses Reflection to print everything out, instead of manually
                // having to assign a form. Future proof. Maybe. 20% chance. 10%?
                UseReflection(buildedExcercise);

                Console.WriteLine("Is the information correct which you wrote down? (y/n): ");
                string answer = Console.ReadLine();

                if (String.IsNullOrEmpty(answer))
                {
                    Console.WriteLine("\n\nNo valid answer was given. Try again.");
                }

                switch (answer)
                {
                    case "y":
                        return buildedExcercise;

                    case "n":
                        Console.WriteLine("Then we're starting again.");
                        continue;
                }
            }        }

        public void UseReflection(Excercise buildedExcercise)
        {
            PropertyInfo[] properties = typeof(Excercise).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(buildedExcercise);
                string name = property.Name;
                Console.WriteLine($"{name} = {value}");
            }
        }
    }
}