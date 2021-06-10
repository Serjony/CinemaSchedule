using System;

namespace CinemaSchedule
{
    public class PrintSchedule
    {
        public static void PrintTable(TimeTable bestTable)
        {
            Console.WriteLine("\nОптимальное расписание работы зала кинотеатра:");
            
                DateTime startFilmTime = new DateTime(2021, 05, 13, 10, 00, 00);
                Console.WriteLine($"\nРасписание работы зала :");
                foreach (var currentFilm in bestTable.Table)
                {
                    DateTime endFilmTime = startFilmTime.AddMinutes(currentFilm.Duration);
                    Console.WriteLine($"{startFilmTime.ToShortTimeString()}-{endFilmTime.ToShortTimeString()}  {currentFilm.Name}, продолжительность:{currentFilm.Duration} минут");
                    startFilmTime = endFilmTime;
                }
                Console.WriteLine($"Оставшееся свободное время в зале: {bestTable.FreeTime} минут");
        }
    }
}
