using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    // Class for defining a series of excercises (e.g. Push Day Workout)
    internal class Workout
    {
        public string Name { get; set; } = "Push Day Workout";
        public string Description { get; set; } = "A workout that takes care of your Chest, Triceps and Shoulders. Used in combination with a Push Pull Legs workout.";

        public List<Excercise> Excercises { get; set; } = new List<Excercise>();

        public void AddExcercise (Excercise excercise)
        {
            Excercises.Add(excercise);
        }

        public List<Workout> Workouts { get; set; } = new List<Workout>();

        public void AddWorkout (Workout workout)
        {
            Workouts.Add(workout);
        }
    }
}
