using System.Collections.Generic;

namespace CinemaSchedule
{
    public class GraphTree
    {
        private List<Film> movieList;
        private int movieListDuration = 0;
        private Node root;
        public List<TimeTable> allTablesWithFreeTime = new List<TimeTable>();

        public GraphTree()
        {

        }
        public GraphTree(List<Film> list, int FreeTime)
        {
            movieList = list;
            movieListDuration = CalcMovieListDuration(movieList);
            if (movieListDuration < CinemaWorkTime.CinemaWorkingTime)
            {
                root = new Node(FreeTime, new List<Film>(movieList));
                root.RemainingTime -= movieListDuration;
            }
            else
            {
                root = new Node(FreeTime, new List<Film>());
            }
        }

        public int CalcMovieListDuration(List<Film> movieList)
        {
            foreach (var movie in movieList)
            {
                movieListDuration += movie.Duration;
            }
            return movieListDuration;
        }
        public void CreateTree()
        {
            CreateGraph(root);
        }

        public void CreateGraph(Node node)
        {
            foreach (var film in movieList)
            {
                if (node.RemainingTime >= film.Duration)
                {
                    List<Film> tmp = new List<Film>(node.CurrentFilms);
                    tmp.Add(film);
                    Node newNode = new Node(node.RemainingTime - film.Duration, tmp);
                    node.AddNext(newNode);
                    CreateGraph(newNode);
                }
            }
            TimeTable currentTable = new TimeTable();
            currentTable.FreeTime = node.RemainingTime;
            currentTable.Table = new List<Film>();
            foreach (var value in node.CurrentFilms)
            {
                currentTable.Table.Add(new Film { Name = value.Name, Duration = value.Duration });
            }
            allTablesWithFreeTime.Add(currentTable);
        }

        public TimeTable FindOptimalTable()
        {
            List<TimeTable> result = new List<TimeTable>(allTablesWithFreeTime);
            int minFreeTime = CinemaWorkTime.CinemaWorkingTime;
            TimeTable optimalTable = new TimeTable();
            foreach (var item in result)
            {
                if (minFreeTime > item.FreeTime)
                {
                    minFreeTime = item.FreeTime;
                    optimalTable = item;
                }
            }
            return optimalTable;
        }
    }
}




