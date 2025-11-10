namespace ConsoleApp1
{
    // Class for defining a seperate workout (e.g. Bench Press)
    public class Excercise : ICloneable
    {
        public string Name { get; set; } = "DUMMY NAME";
        public string Description { get; set; } = "IF YOU READ THIS, SOMETHING WENT WROONG";
        public int DifficultyLevel { get; set; } = 1; // 1 = Beginner, 2 = Normal, 3 = Advanced.

        public List<string> PrimaryFocus { get; set; } = [];
        public List<string> SecondaryFocus { get; set; } = [];
        public string ExcerciseInformation { get; set; } = "Dummy Information";
        public List<string> ExecutionSteps { get; set; } = []; // in steps, 1 2 3 etc

        public byte[]? Image { get; set; } = null;
        public string VideoURL { get; set; } = "https://www.youtube.com/watch?v=dQw4w9WgXcQ"; //maybe add a mp4 option? where do we save this though..

        public int Sets { get; set; } = 3;
        public int Reps { get; set; } = 12;
        public TimeSpan RecommendedRestTime = TimeSpan.FromSeconds(60);

        public List<string> EquipmentNeeded { get; set; } = [];

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}