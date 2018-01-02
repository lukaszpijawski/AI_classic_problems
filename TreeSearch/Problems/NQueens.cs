using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka
{
    public class NQueens : IProblem<byte[]>
    {
        #region Fields
        private byte[] _initial;
        private static List<byte> _columnNumbers;
        PointEqualityComparer pointEqualityComparer = new PointEqualityComparer();
        private HashSet<Point>[,] tableOfChecksOnDiagonal;
        #endregion

        #region Properties
        public byte[] InitialState
        {
            get
            {
                return _initial;
            }
        }
        #endregion

        #region Constructors

        public NQueens() : this(8)
        {
        }

        public NQueens(int size) : this(MakeInitialState(size))
        {
        }        

        public NQueens(byte[] initial)
        {
            this._initial = initial;
            PrintTable(initial);
            if (!IsSolvable(initial))
            {
                throw new UnsolvableProblemException("Ten problem jest nierozwiązywalny");
            }
            _columnNumbers = GetColumnNumbers(initial.Length);
            tableOfChecksOnDiagonal = CreateTableOfChecksOnDiagonal(_initial.Length);
        }
        #endregion

        #region Initializers
        private static byte[] MakeInitialState(int size)
        {
            if (size < 1)
            {
                throw new SizeOfProblemException("Liczba hetmanów musi wynosić co najmniej 1");
            }

            var columnNumbers = GetColumnNumbers(size);
            var random = new Random();
            byte[] initial = new byte[size];

            for (int j = 0; j < size; j++)
            {
                var indeks = random.Next(0, columnNumbers.Count - 1);
                initial[j] = columnNumbers[indeks];
                columnNumbers.RemoveAt(indeks);
            }
            return initial;
        }

        private static List<byte> GetColumnNumbers(int size)
        {
            var numeryKolumn = new List<byte>(size);
            for (int i = 0; i < size; i++)
            {
                numeryKolumn.Add((byte)(i));
            }
            return numeryKolumn.ToList();
        }

        private HashSet<Point>[,] CreateTableOfChecksOnDiagonal(int length)
        {
            var tableOfChecks = new HashSet<Point>[length, length];
            for (int i = 0; i < tableOfChecks.GetLength(0); i++)
            {
                for (int j = 0; j < tableOfChecks.GetLength(1); j++)
                {
                    var set = new HashSet<Point>();

                    for (int k = 0; k < length; k++)
                    {
                        var y = j - (i - k);
                        if (y >= 0 && y < length)
                        {
                            set.Add(new Point(k, y));
                        }
                        y = j + (i - k);
                        if (y >= 0 && y < length)
                        {
                            set.Add(new Point(k, y));
                        }
                    }
                    tableOfChecks[i, j] = new HashSet<Point>(set.GroupBy(a => new { a.X, a.Y }).Select(a => a.Last()));
                }
            }

            return tableOfChecks;
        }
        #endregion

        #region PrintTable
        private void PrintTable(byte[] table)
        {
            foreach (var element in table)
            {
                Console.Write((element + 1) + " ");
            }
            Console.WriteLine();
        }
        #endregion

        #region IsSolvable
        private static bool IsSolvable(byte[] state)
        {
            return state.Length > 3;
        }
        #endregion

        #region Checks calculating
        private int CalculateNumberOfChecks(byte[] state)
        {
            int numberOfChecks = 0;
            byte[] tymczasowystate = state.ToArray();
            var stateAsHashSet = new HashSet<Point>();
            for (byte i = 0; i < state.Length; i++)
            {
                var pole = new Point(i, state[i]);
                stateAsHashSet.Add(pole);
            }

            numberOfChecks += CalculateNumberOfDiagonalChecks(stateAsHashSet);
            numberOfChecks += CalculateNumberOfColumnChecks(state);

            return numberOfChecks;
        }

        private int CalculateNumberOfColumnChecks(byte[] state)
        {
            int numberOfChecks = 0;
            foreach (var queen in state)
            {
                numberOfChecks += state.Where(a => a == queen).Count() - 1;
            }

            return numberOfChecks / 2;
        }

        private int CalculateNumberOfDiagonalChecks(HashSet<Point> stateAsHashSet)
        {
            int liczbaSzachowan = 0;
            foreach (var hetman in stateAsHashSet)
            {
                liczbaSzachowan += tableOfChecksOnDiagonal[hetman.X, hetman.Y].Where(a => stateAsHashSet.Contains(a, pointEqualityComparer)).Count() - 1;
            }

            return liczbaSzachowan / 2;
        }
        #endregion

        #region Expand
        public IList<byte[]> Expand(byte[] state)
        {
            var possibleStates = new List<byte[]>();
            for (int i = 0; i < state.Length; i++)
            {               
                for (int j = 0; j < state.Length; j++)
                {
                    var newState = state.ToArray();
                    if (newState[i] != j)
                    {
                        newState[i] = (byte)j;
                        possibleStates.Add(newState);
                    }
                }
            }
            return possibleStates;
        }
        #endregion

        #region Comparing states
        public bool IsGoal(byte[] state)
        {
            return CalculateNumberOfChecks(state) == 0;
        }
        
        public bool AreStatesTheSame(byte[] state1, byte[] state2)
        {
            if (state1.Length != state2.Length)
            {
                return false;
            }
            for (int i = 0; i < state1.Length; i++)
            {
                if (state1[i] != state2[i])
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region Calculating cost
        public int CompareStatesPriority(byte[] state1, byte[] state2)
        {
            var costOfState1 = CalculateNumberOfChecks(state1);
            var costOfState2 = CalculateNumberOfChecks(state2);

            if (costOfState1 > costOfState2) return -1;
            if (costOfState1 < costOfState2) return 1;
            return 0;
        }

        public int CompareStatesPriorityWithPathCost(byte[] state1, byte[] state2, double state1PathCost, double state2PathCost)
        {
            var costOfState1 = CalculateNumberOfChecks(state1) + state1PathCost;
            var costOfState2 = CalculateNumberOfChecks(state2) + state2PathCost;

            if (costOfState1 > costOfState2) return -1;
            if (costOfState1 < costOfState2) return 1;
            return 0;
        }

        public double CalculateCostToNextState(byte[] state1, byte[] state2)
        {
            return Math.Abs(CalculateNumberOfChecks(state1) - CalculateNumberOfChecks(state2));
        }

        public double CalculateCostToGoal(byte[] state)
        {
            return CalculateNumberOfChecks(state);
        }
        #endregion
    }
}
