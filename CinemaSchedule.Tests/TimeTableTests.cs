//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NUnit.Framework;

namespace CinemaSchedule.Tests
{
    public class GraphTreeTests
    {
        [TestCase(1, 270)]
        [TestCase(2, 550)]
        [TestCase(3, 470)]
        public void CalcMovieListDuration_WhenListRecieved_ReturnSumOfMoviesDuration(int mockNumber, int expected)
        {
            GraphTree graph = new GraphTree();
            List<Film> movieList = GraphTreeMock.GetMock(mockNumber);
            int actual = graph.CalcMovieListDuration(movieList);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void FindOptimalTable_WhenOptimalTableFind_CheckThatTimeTableContainsAllFilms(int mockNumber)
        {
            List<Film> movieList = GraphTreeMock.GetMock(mockNumber);
            GraphTree graph = new GraphTree(movieList, CinemaWorkTime.CinemaWorkingTime);
            Node rootNode = new Node(CinemaWorkTime.CinemaWorkingTime, movieList);
            graph.CreateGraph(rootNode);
            TimeTable timeTable = graph.FindOptimalTable();
            foreach (var movie in movieList)
            {
                Assert.IsTrue(timeTable.Table.Contains(movie));
            }
            return;
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void FindOptimalTable_WhenOptimalTableFinded_CheckThatFreeTimeHasMinValue(int mockNumber)
        {
            int actual = CinemaWorkTime.CinemaWorkingTime;
            int expected;
            List<Film> movieList = GraphTreeMock.GetMock(mockNumber);
            GraphTree graph = new GraphTree(movieList, CinemaWorkTime.CinemaWorkingTime);
            Node rootNode = new Node(CinemaWorkTime.CinemaWorkingTime, movieList);
            graph.CreateGraph(rootNode);
            TimeTable timeTable = graph.FindOptimalTable();
            foreach (var table in graph.allTablesWithFreeTime)
            {
                if (actual > table.FreeTime)
                {
                    actual = table.FreeTime;
                }
            }
            expected = timeTable.FreeTime;
            Assert.AreEqual(actual, expected);
        }
    }
    public static class GraphTreeMock
    {
        public static List<Film> GetMock(int number)
        {
            List<Film> result = new List<Film>();
            switch (number)
            {
                case 1:
                    result.Add(new Film { Name = "Time", Duration = 120 });
                    result.Add(new Film { Name = "21", Duration = 90 });
                    result.Add(new Film { Name = "KinKong", Duration = 60 });
                    break;
                case 2:
                    result.Add(new Film { Name = "Beginning", Duration = 310 });
                    result.Add(new Film { Name = "Inception", Duration = 240 });
                    break;
                case 3:
                    result.Add(new Film { Name = "Terminator 2", Duration = 200 });
                    result.Add(new Film { Name = "Titanik", Duration = 120 });
                    result.Add(new Film { Name = "Avatar", Duration = 90 });
                    result.Add(new Film { Name = "Godzilla", Duration = 60 });
                    break;
            }
            return result;
        }
    }
}