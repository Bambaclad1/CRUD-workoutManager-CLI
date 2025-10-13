using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    // Class for defining a seperate workout (e.g. Bench Press)
    public class Excercise
    {
        public string Name { get; set; } = "Dummy Information";
        public string Description { get; set; } = "Dummy Description";
        public int DifficultyLevel { get; set; } = 1; // 1 = Beginner, 2 = Normal, 3 = Advanced.

        public List<string> PrimaryFocus { get; set; } = ["Chest Muscles"];
        public List<string> SecondaryFocus { get; set; } = ["Shoulder Muscles", "Triceps"];
        public string ExcerciseInformation { get; set; } = "Dummy Information";
        public List<string> ExecutionSteps { get; set; } = ["Feet close", "Puff chest up", "Lift barbell", "controlled to your upper chest", "now push up in the way of the barbell bla bla curved direction"]; // in steps, 1 2 3 etc

        public byte[]? Image { get; set; } = null;
        public string VideoURL { get; set; } = "https://www.youtube.com/watch?v=dQw4w9WgXcQ"; //maybe add a mp4 option? where do we save this though..

        public int Sets { get; set; } = 3;
        public int Reps { get; set; } = 12;
        public TimeSpan RecommendedRestTime = TimeSpan.FromSeconds(60);

        public List<string> EquipmentNeeded { get; set; } = ["Barbell", "Bench"];
    }
}