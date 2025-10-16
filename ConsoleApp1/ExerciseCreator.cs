using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Transactions;

namespace ConsoleApp1
{
    public interface IBuilder
    {
        void AskName();

        void AskDescription();

        void AskDifficultyLevel();

        void AskPrimaryFocus();

        void AskSecondaryFocus();

        void AskExcerciseInformation();

        void AskExecutionSteps();

        void AskImage();

        void AskVideoURL();

        void AskSets();

        void AskReps();

        void AskRecommendedRestTime();

        void AskEquipmentNeeded();
    }

    public class ExcerciseBuilder : IBuilder
    {
        private Excercise _excercise = new Excercise();

        public ExcerciseBuilder()
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

        public void AskDescription()
        {
            Console.Write("\nEnter your Excercise Description: ");
            _excercise.Description = Console.ReadLine();
        }

        public void AskDifficultyLevel()
        {
            int result;

            while (true)
            {
                Console.Write("\nEnter your Excercise Difficulty Level (1-3): ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out result))
                {
                    Console.Write("\n\nThat seems to be a invalid input. Try again.");
                    continue;
                }

                if (result < 1 || result > 3)
                {
                    Console.WriteLine("\n\nDifficulty must be between 1 and 3. Try again.");
                    continue;
                }

                break;
            }

            _excercise.DifficultyLevel = result;
        }

        public void AskPrimaryFocus()
        {
            Console.Write("\nWhat is the primary muscle group this excercise target: ");
            _excercise.PrimaryFocus.Add(Console.ReadLine());

            AdditionalParametersSelectorLoop("Do you want to add more primary muscles this group targets? (y/n): ", () => _excercise.PrimaryFocus);
        }

        public void AskSecondaryFocus()
        {
            Console.Write("\nWhat are the secondary muscle groups this excercise targets: ");
            _excercise.SecondaryFocus.Add(Console.ReadLine());

            AdditionalParametersSelectorLoop("Do you want to add more secondary muscles this group targets? (y/n): ", () => _excercise.SecondaryFocus);
        }

        public void AskExcerciseInformation()
        {
            while (true)
            {
                Console.WriteLine("\nPlease add some information about this excercise. Here is a example: ");

                string example = """
                Bench Press description example:
                The bench press is a compound upper-body strength exercise where you lie on a bench and press a weight,
                typically a barbell or dumbbells, from your chest upward to full arm extension, then lower it back down.
                It primarily works the chest (pectorals), shoulders (deltoids), and triceps, with core and back muscles providing stabilization.
                Proper form is crucial to avoid shoulder injury and ensure effective muscle recruitment.
                """;
                Console.WriteLine("\n" + example);

                Console.WriteLine("\nNow goes your description (be wary of copypasting...): ");
                string Description = Console.ReadLine();

                Console.WriteLine("\nYour description looks like this:");
                Console.WriteLine(Description);
                Console.Write("\n\nDoes that look good to you? (y/n):");
                string userinput = Console.ReadLine();

                if (String.IsNullOrEmpty(userinput))
                {
                    Console.WriteLine("\n\nNo valid answer was given. Try again.");
                }

                switch (userinput)
                {
                    case "y":
                        _excercise.ExcerciseInformation = Description;
                        return;

                    case "n":
                        Console.WriteLine("\n\nLets try again than.");
                        continue;

                    default:
                        Console.WriteLine("\n\nNo valid answer was given (y/n). Try again.");
                        continue;
                }
            }
        }

        public void AskExecutionSteps()
        {
            Console.WriteLine("\nPlease add the right execution steps for this excericse.");

            string example = """
                Example for the bench press:
                Example for the bench press:

                1. Get Set Up
                   - Lie on the bench with eyes under the bar.
                   - Plant feet firmly and squeeze shoulder blades together.

                2. Unrack the Bar
                   - Grip slightly wider than shoulder-width.
                   - Lift the bar out and hold it above your chest with locked elbows.

                3. Lower the Bar
                   - Inhale and lower slowly.
                   - Keep elbows tucked and touch the bar lightly to your chest.

                4. Press the Bar Up
                   - Exhale and press in an arc.
                   - Drive through your chest and feet, locking out at the top.

                5. Repeat
                   - Control each descent.
                   - After your final rep, rack the bar safely.
                """;
            Console.WriteLine("\n" + example);

            Console.WriteLine("\nWhile writing, type '!next' to write the next step");
            Console.WriteLine("\nAnd write '!done' to complete the steps.");
            Console.WriteLine("\nNow goes your execution steps::\n");

            List<string> executionSteps = new List<string>();
            int stepnumber = 1;

            while (true)
            {
                Console.Write($"\n Step {stepnumber} ");
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Step cannot be null or empty. Try again.");
                    continue;
                }

                if (input.Equals("!done", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\nDone.");
                    break;
                }

                if (input.Equals("!next", StringComparison.OrdinalIgnoreCase))
                {
                    stepnumber++;
                    continue;
                }
                executionSteps.Add($"Step {stepnumber}. {input}");

                foreach (string step in executionSteps)
                {
                    Console.WriteLine(step);
                }
                // todo: see what datatype is best, list<string> or string
                _excercise.ExecutionSteps = executionSteps;
            }
        }

        public void AskImage()
        {
            // how the f*ck do we add a image from a damn cli application? to-do.
            _excercise.Image = null;
        }

        public void AskVideoURL()
        {
            Console.Write("\nPlease enter a Video URL (direct .mp4 or youtube) demonstrating this excercise. Type '!skip' to skip this part: ");
            string input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input) || input.Equals("!skip", StringComparison.OrdinalIgnoreCase))
            {
                //null
            }
            else
            {
                _excercise.VideoURL = input;
            }
        }

        public void AskSets() //RESTRICTIONS NOT WORKING!
        {
            while (true)
            {
                Console.Write("\nPlease enter the amount of sets this excercise will have in numbers: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int output))
                {
                    Console.Write("\n\nInvalid answer. Please answer in a valid integer.");
                    continue;
                }

                if (output >= 1 && output <= 11)
                {
                    _excercise.Sets = output;
                    break;
                }
                else
                {
                    Console.Write("\n\nAnswer must be between values 1-10, " + output + " is invalid.");
                    continue;
                }
            }
        }

        public void AskReps()
        {
            while (true)
            {
                Console.Write("\nPlease enter the amount of reps this excercise will have in numbers: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int output))
                {
                    Console.Write("\n\nInvalid answer. Please answer in a valid integer.");
                    continue;
                }

                if (output > 1 && output <= 30)
                {
                    _excercise.Reps = output;
                    break;
                }
                else
                {
                    Console.Write("\n\nAnswer must be between values 1-30, " + output + " is invalid.");
                    continue;
                }
            }
        }

        public void AskRecommendedRestTime()
        {
            while (true)
            {
                Console.Write("\nWhat is the recommended rest time for this excercise? (Answer in seconds', unsure? enter 60.): ");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Answer is invalid. Try again.");
                    continue;
                }

                try
                {
                    if (!int.TryParse(input, out int output))
                    {
                        Console.Write("\n\nSomething went wrong attempting to parse the integer. Please try again.");
                        continue;
                    }
                    else
                    {
                        _excercise.RecommendedRestTime = TimeSpan.FromSeconds(output);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.Write("\n\n" + ex.ToString());
                    continue;
                }
            }
        }

        public void AskEquipmentNeeded()
        {
            Console.Write("\nWhat kind of equipment does your excercise need? Type 'nothing' if its a body weight excercise: ");
            string input = Console.ReadLine();

            if (input.Equals("nothing", StringComparison.OrdinalIgnoreCase))
            {
                //continue
            }
            else
            {
                _excercise.EquipmentNeeded.Add(input);
                while (true)
                {
                    Console.WriteLine("\nIs there more equipment involved? (y/n): ");
                    string yn = Console.ReadLine();

                    if (yn.Equals("n", StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }

                    Console.Write("\nPlease write down the additional equipment involved: ");
                    _excercise.EquipmentNeeded.Add(Console.ReadLine());
                }
            }
        }

        public void AdditionalParametersSelectorLoop(string Message, Func<List<String>> getList)
        // Instead of having to go through the pain of duplicating your method, you can refactor it like this! #cleancodegang
        {
            while (true)
            {
                Console.Write("\n" + Message);
                string userinput = Console.ReadLine();

                if (String.IsNullOrEmpty(userinput))
                {
                    Console.WriteLine("\n\nNo valid answer was given. Try again.");
                }

                switch (userinput)
                {
                    case "y":
                        Console.Write("\nPlease write down your additional answer: ");
                        getList().Add(Console.ReadLine());
                        continue;
                    case "n":
                        return;

                    default:
                        Console.WriteLine("\n\nNo valid answer was given (y/n). Try again.");
                        continue;
                }
            }
        }

        public Excercise GetProduct()
        {
            Excercise result = this._excercise;

            this.Reset();

            return result;
        }
    }

    public class ExerciseCreator
    {
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
                builder.AskDescription();
                builder.AskDifficultyLevel();
                builder.AskPrimaryFocus();
                builder.AskSecondaryFocus();
                builder.AskExcerciseInformation();
                builder.AskExecutionSteps();
                builder.AskImage();
                builder.AskVideoURL();
                builder.AskSets();
                builder.AskReps();
                builder.AskRecommendedRestTime();
                builder.AskEquipmentNeeded();
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
            }
        }

        public static void UseReflection(Excercise buildedExcercise)
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