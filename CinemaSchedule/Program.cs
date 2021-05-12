using System;
using System.Collections.Generic;

namespace CinemaSchedule
{
    class Program
    {
        static void Main(string[] args)
        {
            int cinemaWorkingMinutes = 840;
            List<Film> userFilmsList = new List<Film>();
            int filmCount;

            Console.WriteLine("Введите количество фильмов в прокате:");
            filmCount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Для каждого фильма из проката необходимо предоставить более подробную информацию о нем (название и продолжительности фильма в минутах):");
            for (int i = 1; i <= filmCount; i++)
            {
                Console.WriteLine($"Введите название фильма № {i}:");
                string fName = Console.ReadLine();
                Console.WriteLine($"Введите длительность фильма №{i} (в минутах):");
                int fDuration = Convert.ToInt32(Console.ReadLine());
                userFilmsList.Add(new Film { Duration = fDuration, Name = fName });
            }

            GraphTree cinemaTable = new GraphTree(userFilmsList, cinemaWorkingMinutes);
            cinemaTable.CreateTree();
            TimeTable optimalTable = cinemaTable.FindOptimalTable();
            PrintSchedule.PrintTable(optimalTable);
        }
    }
}
