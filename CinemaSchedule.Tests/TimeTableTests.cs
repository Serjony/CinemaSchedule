using NUnit.Framework;
using System.Collections.Generic;



namespace CinemaSchedule.Tests
{
    public class GraphTreeTests
    {

        [TestCase(1, 193)]
        [TestCase(2, 550)]
        [TestCase(3, 470)]
        public void CalcMovieListDuration_WhenListRecived_ReturnSummOfMoviesDuration(int mockNumber, int expected)
        {
            GraphTree graph = new GraphTree();
            List<Film> movieList = GraphTreeMock.GetMock(mockNumber);

            int actual = graph.CalcMovieListDuration(movieList);

            Assert.AreEqual(expected, actual);
        }
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void GraphTree_WhenList_ReturnSummOfMoviesDuration(int mockNumber)
        {
            List<Film> movieList = GraphTreeMock.GetMock(mockNumber);
            GraphTree graph = new GraphTree(movieList, 840);
            Node rootNode = new Node(840, movieList);
            graph.CreateGraph(rootNode);
            TimeTable timeTable = graph.FindOptimalTable();
            foreach (var movie in movieList)
            {
                Assert.IsTrue(timeTable.Table.Contains(movie));
            }

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
                    result.Add(new Film { Name = "Time", Duration = 43 });
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