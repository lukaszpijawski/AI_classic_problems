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

        static void PrintTable(byte[] table)
        {

        }

        private static void Heapify(int[] A, int i, int heapSize)
        {
            int l, r, x, largest;
            Func<int, int> left = a => 2 * a + 1;
            Func<int, int> right = a => 2 * a + 2;

            l = left(i);
            r = right(i);

            largest = (l < heapSize && A[l] > A[i]) ? l : i;
            largest = (r < heapSize && A[r] > A[largest]) ? r : largest;

            if (largest != i)
            {
                x = A[i];
                A[i] = A[largest];
                A[largest] = x;
                Heapify(A, largest, heapSize);
            }
        }


        static void Main(string[] args)
        {
            byte[,] initial = new byte[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 0, 15 } };
            byte[,] goal = new byte[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 0 } };
            int size = 3;

            try
            {
                var przesuwanka = new Przesuwanka(size);
                var node = TreeSearchWithQueue<byte[, ]>.Search(przesuwanka, new PriorityQueueFringe<Node<byte[,]>>());
                PrintTable(node.NodeState);
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