using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka
{
    class Program
    {
        static void PrintTable(byte[,] table)
        {
            for (int i = 0; i < table.GetLength(0); i++)
            {
                Console.Write("| ");
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    Console.Write(table[i, j] + " ");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine();
        }

        static void PrintTable(IList<byte> table)
        {
            for (int i = 0; i < table.Count; i++)
            {
                Console.Write((table[i] + 1) + " ");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {           
            try
            {
                SolveProblems();
            }
            catch (ElementNotFoundInPrzesuwankaException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (UnsolvableProblemException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Other exception, its source: {0}, message: {1}", e.Source, e.Message);
                if (e.InnerException != null)
                {
                    Console.WriteLine("Inner exception message: " + e.InnerException.Message);
                }
            }
        }    
        
        private static void SolveProblems()
        {
            #region Cities initialization
            #region Cities initialization
            var Oradea = new City("Oradea", new Point(15, 59));
            var Zerind = new City("Zerind", new Point(11, 51));
            var Arad = new City("Arad", new Point(7, 43));
            var Timisoara = new City("Timisoara", new Point(8, 29));
            var Lugoj = new City("Lugoj", new Point(21, 23));
            var Mehadia = new City("Mehadia", new Point(21, 15));
            var Drobeta = new City("Drobeta", new Point(21, 7));
            var Sibiu = new City("Sibiu", new Point(29, 37));
            var Rimnicu = new City("Rimnicu Vilcea", new Point(33, 29));
            var Craiova = new City("Craiova", new Point(37, 5));
            var Fagaras = new City("Fagaras", new Point(47, 36));
            var Pitesti = new City("Pitesti", new Point(50, 20));
            var Giurgiu = new City("Giurgiu", new Point(60, 2));
            var Bucharest = new City("Bucharest", new Point(65, 13));
            var Neamt = new City("Neamt", new Point(66, 52));
            var Urziceni = new City("Urziceni", new Point(75, 17));
            var Iasi = new City("Iasi", new Point(78, 47));
            var Vaslui = new City("Vaslui", new Point(85, 35));
            var Hirsova = new City("Hirsova", new Point(89, 17));
            var Eforie = new City("Eforie", new Point(95, 7));
            #endregion

            #region Adding neighbours
            Oradea.AddNeighbour(Zerind, 71);
            Oradea.AddNeighbour(Sibiu, 151);

            Zerind.AddNeighbour(Oradea, 71);
            Zerind.AddNeighbour(Arad, 75);

            Arad.AddNeighbour(Zerind, 75);
            Arad.AddNeighbour(Sibiu, 140);
            Arad.AddNeighbour(Timisoara, 118);

            Timisoara.AddNeighbour(Arad, 118);
            Timisoara.AddNeighbour(Lugoj, 111);

            Lugoj.AddNeighbour(Timisoara, 111);
            Lugoj.AddNeighbour(Mehadia, 70);

            Mehadia.AddNeighbour(Lugoj, 70);
            Mehadia.AddNeighbour(Drobeta, 75);

            Drobeta.AddNeighbour(Mehadia, 75);
            Drobeta.AddNeighbour(Craiova, 120);

            Sibiu.AddNeighbour(Arad, 140);
            Sibiu.AddNeighbour(Oradea, 151);
            Sibiu.AddNeighbour(Fagaras, 99);
            Sibiu.AddNeighbour(Rimnicu, 80);

            Rimnicu.AddNeighbour(Sibiu, 80);
            Rimnicu.AddNeighbour(Pitesti, 97);
            Rimnicu.AddNeighbour(Craiova, 146);

            Craiova.AddNeighbour(Drobeta, 120);
            Craiova.AddNeighbour(Rimnicu, 146);
            Craiova.AddNeighbour(Pitesti, 138);

            Fagaras.AddNeighbour(Sibiu, 99);
            Fagaras.AddNeighbour(Bucharest, 211);

            Pitesti.AddNeighbour(Rimnicu, 97);
            Pitesti.AddNeighbour(Bucharest, 101);
            Pitesti.AddNeighbour(Craiova, 138);

            Giurgiu.AddNeighbour(Bucharest, 90);

            Bucharest.AddNeighbour(Pitesti, 101);
            Bucharest.AddNeighbour(Fagaras, 211);
            Bucharest.AddNeighbour(Urziceni, 85);
            Bucharest.AddNeighbour(Giurgiu, 90);

            Neamt.AddNeighbour(Iasi, 87);

            Urziceni.AddNeighbour(Bucharest, 85);
            Urziceni.AddNeighbour(Vaslui, 142);
            Urziceni.AddNeighbour(Hirsova, 98);

            Iasi.AddNeighbour(Neamt, 87);
            Iasi.AddNeighbour(Vaslui, 92);

            Vaslui.AddNeighbour(Iasi, 92);
            Vaslui.AddNeighbour(Urziceni, 142);

            Hirsova.AddNeighbour(Urziceni, 98);
            Hirsova.AddNeighbour(Eforie, 86);

            Eforie.AddNeighbour(Hirsova, 86);
            #endregion

            List<City> cities = new List<City>
            {
                Oradea, Zerind, Arad, Timisoara, Lugoj, Mehadia, Drobeta, Sibiu, Rimnicu, Craiova, Fagaras, Pitesti, Giurgiu, Bucharest, Neamt, Urziceni, Iasi, Vaslui, Hirsova, Eforie
            };
            #endregion

            var queensFringe = new PriorityQueueFringe<Node<byte[]>>();
            var romaniaFringe = new PriorityAStarFringe<Node<City>>();
            var przesuwankaFringe = new PriorityQueueFringe<Node<byte[,]>>();

            SolveNQueens(queensFringe, 8);
            SolveRomaniaMap(romaniaFringe, Arad, Bucharest, cities);
            SolvePrzesuwanka(przesuwankaFringe, 4);
        }

        private static void SolvePrzesuwanka(IFringe<Node<byte[,]>> fringe, int problemSize)
        {
            Console.WriteLine("Przesuwanka {0} x {0}", problemSize);

            var przesuwanka = new Przesuwanka(problemSize);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var node = TreeSearchWithQueue<byte[,]>.Search(przesuwanka, fringe);
            watch.Stop();
            if (node == null)
            {
                Console.WriteLine("Solution not found");
            }
            else
            {
                Console.WriteLine("Solution:");
                PrintTable(node.NodeState);
                var trace = node.ListOfNodes;
                Console.WriteLine("Number of steps: " + trace.Count);                
            }            
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("\nUsed fringe: " + fringe.GetName());
            Console.WriteLine("Time elapsed [ms]: " + elapsedMs);
            Console.WriteLine("\n");
        }

        private static void SolveNQueens(IFringe<Node<byte[]>> fringe, int problemSize)
        {
            Console.WriteLine("{0} queens problem", problemSize);

            var nQueens = new NQueens(problemSize);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var node = TreeSearchWithQueue<byte[]>.Search(nQueens, fringe);
            watch.Stop();
            if (node == null)
            {
                Console.WriteLine("Solution not found");
            }
            else
            {                
                Console.WriteLine("Solution:");
                PrintTable(node.NodeState);
                var trace = node.ListOfNodes;
                Console.WriteLine("Number of steps: " + trace.Count);                
            }
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("\nUsed fringe: " + fringe.GetName());
            Console.WriteLine("Time elapsed [ms]: " + elapsedMs);
            Console.WriteLine("\n");
        }

        private static void SolveRomaniaMap(IFringe<Node<City>> fringe, City initial, City goal, List<City> cities)
        {
            Console.WriteLine("Romania map problem");
            Console.WriteLine("Trace " + initial.Name + " - " + goal.Name + ": \n");

            var romaniaMap = new RomaniaMap(initial, goal, cities);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var node = TreeSearchWithQueue<City>.Search(romaniaMap, fringe);
            watch.Stop();
            if (node == null)
            {
                Console.WriteLine("Solution not found");
            }
            else
            {                
                Console.WriteLine("Solution:");
                var trace = node.ListOfNodes;
                foreach (var city in trace)
                {
                    Console.WriteLine(city.Name);
                }
                Console.WriteLine("\nNumber of steps: " + trace.Count);
                Console.WriteLine("Total cost: " + node.PathCost);
            }
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("\nUsed fringe: " + fringe.GetName());
            Console.WriteLine("Time elapsed [ms]: " + elapsedMs);
            Console.WriteLine("\n");
        }
    }
}