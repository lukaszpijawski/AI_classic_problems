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
            Console.WriteLine(table.Count);
            for (int i = 0; i < table.Count; i++)
            {
                Console.Write((table[i] + 1) + " ");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            byte[,] initial = new byte[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 15, 14, 0 } };
            byte[,] goal = new byte[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 0 } };

            byte[,] initial2 = new byte[,] { { 1, 2 }, { 0, 3 } };
            byte[,] goal2 = new byte[,] { { 1, 2 }, { 3, 0 } };

            try
            {
                var fringe = new PriorityQueueFringe<Node<byte[]>>();
                SolveNQueens(fringe, 8);
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

        private static void SolvePrzesuwanka(IFringe<Node<byte[,]>> fringe, int problemSize)
        {
            var przesuwanka = new Przesuwanka(problemSize);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var node = TreeSearchWithQueue<byte[,]>.Search(przesuwanka, fringe);
            watch.Stop();
            PrintTable(node.NodeState);
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Time after {1} {0}", elapsedMs, fringe.GetName());
        }

        private static void SolveNQueens(IFringe<Node<byte[]>> fringe, int problemSize)
        {
            var nQueens = new NQueens(problemSize);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var node = TreeSearchWithQueue<byte[]>.Search(nQueens, fringe);
            watch.Stop();
            PrintTable(node.NodeState);
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Time after {1} {0}", elapsedMs, fringe.GetName());
        }
    }
}