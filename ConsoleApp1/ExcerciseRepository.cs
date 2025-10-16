using System.Text.Json;

namespace ConsoleApp1
{
    public class ExcerciseRepository
    {
        private static readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true,
            AllowTrailingCommas = true,
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

        public static void Read()
        {
            Console.Title = "FWT-CLI ExcerciseRepository";
            ReadExcercises();
        }
    }
}