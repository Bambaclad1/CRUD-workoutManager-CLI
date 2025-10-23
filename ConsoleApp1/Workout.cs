using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    // Class for defining a series of excercises (e.g. Push Day Workout)
    public class Workout
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Excercise> Excercises { get; set; } = new List<Excercise>();

        public void AddExcercise (Excercise excercise)
        {
            Excercises.Add(excercise);
        }
    }
}
