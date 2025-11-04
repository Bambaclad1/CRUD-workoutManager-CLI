using ConsoleApp1.util;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace ConsoleApp1
{
    public class ExcerciseRepository
    {
        private static readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true,
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true
        };

        private static void ReadExcercises()
        {
            try
            {
                string jsonpath = AppDomain.CurrentDomain.BaseDirectory + "\\excerciseFormat.json";
                string content = File.ReadAllText(jsonpath);
                List<Excercise> parsedExcercises = JsonSerializer.Deserialize<List<Excercise>>(content);

                foreach (var ex in parsedExcercises)
                {
                    ExerciseCreator.UseReflection(ex);
                }
                return;
            }
            catch (Exception e)
            {
                throw new FileNotFoundException("Error 404, while trying to access excerciseFormat, it was not found.", e);
            }
        }

        public void Edit()
        {
            WorkoutRepository workoutRepository = new();
            ExerciseCreator exerciseCreator = new();
            Excercise editedExercise = new();
            Excercise newEx = new();
            List<int> count;

            var exerciseObject = workoutRepository.ReadExercises();
            count = workoutRepository.AskUserInput("Exercices and numbers have been printen down. Please select a exercise to modify.", exerciseObject.excerciseCount, true);

            int index = count[0] - 1;
            Console.Write("\nWould you like to view all current exercise information? (y/n):");
            char yn = Console.ReadKey().KeyChar;

            if (char.ToLowerInvariant(yn) == 'y')
            {
                var newExercise = exerciseObject.parsedExcercises[index];
                ExerciseCreator.UseReflection(newExercise);
            }
            Console.WriteLine("\nPress any key to enter the edit menu...");
            Console.ReadKey();

            newEx = EditMenu(exerciseObject.parsedExcercises[index]);

            exerciseObject.parsedExcercises[index] = newEx;

            Console.WriteLine(exerciseObject.parsedExcercises[index].Name);
            JsonNode jsonnode = JsonNode.Parse(ReturnJsonRepository());
            jsonnode.AsArray().RemoveAt(index);

            jsonnode.AsArray().Add(newEx);
            List<Excercise>? exercises = jsonnode.Deserialize<List<Excercise>>(_options);

            bool isWiped = WipeExerciseRepository();
            if (!isWiped)
                throw new FileNotFoundException("Something went wrong, Exerciseformat was not wiped because this was not found!");

            SaveExercicesList(exercises);
            Console.WriteLine("\nFinished!");
        }

        public string ReturnJsonRepository()
        {
            try
            {
                string jsonpath = AppDomain.CurrentDomain.BaseDirectory + "\\excerciseFormat.json";
                string content = File.ReadAllText(jsonpath);
                List<Excercise> parsedExcercises = JsonSerializer.Deserialize<List<Excercise>>(content);

                return content;
            }
            catch (Exception e)
            {
                throw new FileNotFoundException("Error 404, while trying to access excerciseFormat, it was not found.", e);
            }
        }

        public bool WipeExerciseRepository()
        {
            string jsonPath = AppDomain.CurrentDomain.BaseDirectory + "\\excerciseFormat.json";

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

        //public bool SaveExerciseRepository()
        //{
        //    string jsonPath = AppDomain.CurrentDomain.BaseDirectory + "\\excerciseFormat.json";

        //    if (File.Exists(jsonPath))
        //        Console.WriteLine("Not doing anything because ExcerciseFormat exists...");

        //}

        public Excercise EditMenu(Excercise ex)
        {
            Excercise oldEx = (Excercise)ex.Clone();
            ExcerciseBuilder b = new();

            while (true)
            {
                Console.WriteLine("\n");
                Console.WriteLine($"""
                ----------
                Exercise Edit Menu for {ex.Name}
                ----------
                1. Change Name
                2. Change Description
                3. Change Difficulty Level of the exercise

                4. Change primary targetted muscles engaged in exercise
                5. Change secondary targetted muscles engaged in exercise
                6. Change Exercise Description
                7. Change Exercise execution steps

                8. Change Image (NULL);
                9. Change Video URL for the exercise

                10. Change sets
                11. Change reps
                12. Change recommended rest time
                13. Change equipment needed

                14. See differences between original and modified exercise
                15. Revert changes to original exercise
                0. End editing

                """);
                string input = Console.ReadLine();
                int output = CheckIfValidIntFromString.Check(input, 0, 15);

                switch (output)
                {
                    case 1:
                        b.AskName();
                        ex.Name = b._excercise.Name;
                        break;

                    case 2:
                        b.AskDescription();
                        ex.Description = b._excercise.Description;
                        break;

                    case 3:
                        b.AskDifficultyLevel();
                        ex.DifficultyLevel = b._excercise.DifficultyLevel;
                        break;

                    case 4:
                        b.AskPrimaryFocus();
                        ex.PrimaryFocus = b._excercise.PrimaryFocus;
                        break;

                    case 5:
                        b.AskSecondaryFocus();
                        ex.SecondaryFocus = b._excercise.SecondaryFocus;
                        break;

                    case 6:
                        b.AskExcerciseInformation();
                        ex.ExcerciseInformation = b._excercise.ExcerciseInformation;
                        break;

                    case 7:
                        b.AskExecutionSteps();
                        ex.ExecutionSteps = b._excercise.ExecutionSteps;
                        break;

                    case 8:
                        b.AskImage();
                        ex.Image = b._excercise.Image;
                        break;

                    case 9:
                        b.AskVideoURL();
                        ex.VideoURL = b._excercise.VideoURL;
                        break;

                    case 10:
                        b.AskSets();
                        ex.Sets = b._excercise.Sets;
                        break;

                    case 11:
                        b.AskReps();
                        ex.Reps = b._excercise.Reps;
                        break;

                    case 12:
                        b.AskRecommendedRestTime();
                        ex.RecommendedRestTime = b._excercise.RecommendedRestTime;
                        break;

                    case 13:
                        b.AskEquipmentNeeded();
                        ex.EquipmentNeeded = b._excercise.EquipmentNeeded;
                        break;

                    case 14:
                        Console.WriteLine("\n\nOld Exercise:");
                        ExerciseCreator.UseReflection(oldEx);

                        Console.WriteLine("\n\nNew Exercise:");
                        ExerciseCreator.UseReflection(ex);

                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;

                    case 15:
                        ex = oldEx;
                        Console.WriteLine("\nSuccessfully done!");
                        break;

                    case 0:
                        return ex;

                    case 409:
                        continue;
                }
            }
        }

        public static void SaveExcercises(Excercise buildedExcercise)
        {
            Console.WriteLine("Saving workout, please wait...");
            try
            {
                string jsonPath = AppDomain.CurrentDomain.BaseDirectory + "\\excerciseFormat.json";

                List<Excercise> excercies = new();

                if (File.Exists(jsonPath))
                {
                    string existingContent = File.ReadAllText(jsonPath);

                    if (!string.IsNullOrWhiteSpace(existingContent))
                    {
                        excercies = JsonSerializer.Deserialize<List<Excercise>>(existingContent, _options)
                                    ?? new List<Excercise>();
                    }
                }

                excercies.Add(buildedExcercise);

                string jsonReady = JsonSerializer.Serialize(excercies, _options);

                File.WriteAllText(jsonPath, jsonReady);

                Console.WriteLine("Saved workout successfully!");
            }
            catch (Exception e)
            {
                throw new Exception("Unknown exception, failed to save workout.  ", e);
            }
        }

        public static void SaveExercicesList(List<Excercise> ex)
        {
            try
            {
                string jsonPath = AppDomain.CurrentDomain.BaseDirectory + "\\excerciseFormat.json";

                if (File.Exists(jsonPath))
                {
                    string existingContent = File.ReadAllText(jsonPath);

                    if (!string.IsNullOrWhiteSpace(existingContent))
                    {
                        ex = JsonSerializer.Deserialize<List<Excercise>>(existingContent, _options)
                                    ?? new List<Excercise>();
                    }
                }

                string jsonReady = JsonSerializer.Serialize(ex, _options);

                File.WriteAllText(jsonPath, jsonReady);

                Console.WriteLine("Saved workout successfully!");
            }
            catch (Exception e)
            {
                throw new Exception("Unknown exception, failed to save workout.  ", e);
            }
        }

        public static void Read()
        {
            Console.Title = "FWT-CLI ExcerciseRepository";
            ReadExcercises();
        }
    }
}