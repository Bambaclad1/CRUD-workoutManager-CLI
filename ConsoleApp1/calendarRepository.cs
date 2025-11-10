using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1
{
    internal class CalendarRepository
    {
        public void Create()
        {
            WorkoutRepository workoutRepository = new();
            List<int> count;
            int index;

            var date = new CalDateTime(GetUserDate());

            var workoutObject = workoutRepository.ReadWorkouts();
            count = workoutRepository.AskUserInput("Workouts and numbers have been printen down. Please select a workout to modify.", workoutObject.count, true);
            index = count[0] - 1;


            var calendarEvent = new CalendarEvent
            {
                // If Name property is used, it MUST be RFC 5545 compliant
                Summary = $"Workout {workoutObject.parsedWorkouts[index].Name}", // Should always be present
                Description = SerializeWorkout(workoutObject.parsedWorkouts[index]),
                Start = date,
                End = date.AddHours(2),
            };

            var calendar = new Ical.Net.Calendar();
            calendar.Events.Add(calendarEvent);
            calendar.AddTimeZone(new VTimeZone("Europe/Copenhagen")); // TZ should be added
            var serializer = new CalendarSerializer();
            var serializedCalendar = serializer.SerializeToString(calendar);
            Console.WriteLine(serializedCalendar);

            string path = AppDomain.CurrentDomain.BaseDirectory + "\\test.ics";
            File.WriteAllText(path, serializedCalendar);
            Console.WriteLine($"Written @ {path} successfully!");
        }

        static CalDateTime GetUserDate()
        {
            while (true)
            {
                Console.Write("Enter event date and time(yyyy-MM-dd HH:mm): ");
                string? input = Console.ReadLine();

                if (DateTime.TryParseExact(input, "yyyy-MM-dd hh:mm",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out DateTime result)) 
                {
                    Console.WriteLine("\n\n");
                    return new CalDateTime(result);
                }
                Console.WriteLine("\n\nInvalid Format. Example: 2025-10-11 11:00");
            }
        }

        static string SerializeWorkout(Workout w)
        {
            return $"Your workout has been planned! Name: {w.Name} Description: {w.Description}. Have fun!";
        }
    }
}
