namespace ConsoleApp1
{
    // Class for defining a series of excercises (e.g. Push Day Workout)
    public class Workout : ICloneable
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Excercise> Excercises { get; set; } = new List<Excercise>();

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void AddExcercise(Excercise excercise)
        {
            Excercises.Add(excercise);
        }
    }
}