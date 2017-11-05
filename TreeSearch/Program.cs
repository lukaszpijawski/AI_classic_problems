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
                Console.Write(table[i] + " ");
            }
        }

        

        static void Main(string[] args)
        {
            byte[,] initial = new byte[,] { { 15, 11, 4, 8 }, { 5, 12, 3, 7 }, { 9, 1, 10, 2 }, { 0, 6, 14, 13 } };
            byte[,] goal = new byte[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 0 } };
            
            int size = 4;

            try
            {
                var przesuwanka = new Przesuwanka(3);
                var watch = System.Diagnostics.Stopwatch.StartNew();
                var node = TreeSearchWithQueue<byte[,]>.Search(przesuwanka, new PriorityQueueFringe<Node<byte[,]>>());
                watch.Stop();
                PrintTable(node.NodeState);
                var elapsedMs = watch.ElapsedMilliseconds;
                Console.WriteLine("Time after PriorityQueueFringe {0}", elapsedMs);

                //var przesuwanka2 = new Przesuwanka(przesuwanka.InitialState);
                //var watch2 = System.Diagnostics.Stopwatch.StartNew();
                //var node2 = TreeSearchWithQueue<byte[,]>.Search(przesuwanka2, new QueueFringe<Node<byte[,]>>());
                //watch2.Stop();
                //PrintTable(node2.NodeState);
                //elapsedMs = watch2.ElapsedMilliseconds;
                //Console.WriteLine("Time after QueueFringe {0}", elapsedMs);
            }
            catch (ElementNotFoundInPrzesuwankaException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (UnsolvablePrzesuwankaException e)
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
    }
}