using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka
{
    #region Exceptions
    public class UnsolvablePrzesuwankaException : Exception
    {
        public UnsolvablePrzesuwankaException(string message) : base(message)
        {
        }
    }

    public class ElementNotFoundInPrzesuwankaException : Exception
    {
        public ElementNotFoundInPrzesuwankaException(string message) : base(message)
        {
        }
    }

    public class SizeOfPrzesuwankaLessThanOneException : Exception
    {
        public SizeOfPrzesuwankaLessThanOneException(string message) : base(message)
        {
        }
    }
#endregion

    public class Przesuwanka : IProblem<byte[,]>
    {
        #region Fields
        private byte[,] initial;
        private byte[,] goal;
        Dictionary<byte, Point> goalPositions;
        #endregion

        #region Properties
        public byte[,] InitialState
        {
            get
            {
                return initial;
            }
        }
#endregion

        #region Constructors

        public Przesuwanka() : this(3)
        {
        }

        public Przesuwanka(int size) : this(MakeInitialState(size), MakeGoalState(size))
        {
        }

        public Przesuwanka(byte[,] initial) : this(initial, MakeGoalState(initial.GetLength(0)))
        {
        }

        public Przesuwanka(byte[,] initial, byte[,] goal)
        {            
            this.initial = initial;
            this.goal = goal;
            PrintTable(initial);
            goalPositions = GetStatePositionsOfNumbers(this.goal);
            if (!IsSolvable(initial))
            {
                throw new UnsolvablePrzesuwankaException("Ta przesuwanka jest nierozwiązywalna");
            }
        }
        #endregion

        #region Initializers
        private static byte[,] MakeInitialState(int size)
        {
            if (size < 1)
            {
                throw new SizeOfPrzesuwankaLessThanOneException("Rozmiar przesuwanki musi wynosić co najmniej 1");
            }

            List<byte> setOfNumbers = SetNumberOfElements(size);
            var initial = FillTableRandomlyWithSetOfNumbers(new List<byte>(setOfNumbers), size);
            while (!IsSolvable(initial))
            {
                initial = FillTableRandomlyWithSetOfNumbers(new List<byte>(setOfNumbers), size);
            }
            return initial;
        }

        private static List<byte> SetNumberOfElements(int size)
        {
            List<byte> setOfNumbers = new List<byte>();
            int numberOfElements = size * size;

            for (byte i = 0; i < numberOfElements; i++)
            {
                setOfNumbers.Add(i);
            }
            return setOfNumbers;
        }

        private static byte[,] FillTableRandomlyWithSetOfNumbers(List<byte> setOfNumbers, int size)
        {
            Random random = new Random();
            var table = new byte[size, size];
            for (byte i = 0; i < size; i++)
            {
                for (byte j = 0; j < size; j++)
                {
                    var index = (byte)random.Next(0, setOfNumbers.Count - 1);
                    var chosenNumber = setOfNumbers[index];
                    table[i, j] = chosenNumber;
                    setOfNumbers.Remove(chosenNumber);
                }
            }
            return table;
        }

        private static byte[,] MakeGoalState(int size)
        {
            if (size < 1)
            {
                throw new SizeOfPrzesuwankaLessThanOneException("Rozmiar przesuwanki musi wynosić co najmniej 1");
            }

            var goal = new byte[size, size];
            byte filler = 0;
            for (byte i = 0; i < size; i++)
            {
                for (byte j = 0; j < size; j++)
                {
                    goal[i, j] = filler++;
                }
            }
            return goal;
        }

        private Dictionary<byte, Point> GetStatePositionsOfNumbers(byte[,] state)
        {
            Dictionary<byte, Point> positions = new Dictionary<byte, Point>();

            for (byte i = 0; i < state.GetLength(0); i++)
            {
                for (byte j = 0; j < state.GetLength(1); j++)
                {
                    positions.Add(state[i, j], new Point(i, j));
                }
            }
            return positions;
        }
        #endregion

        #region PrintTable
        private void PrintTable(byte[,] table)
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
        #endregion

        #region IsSolvable
        private static bool IsSolvable(byte[,] state)
        {
            int inversionsCounter = CountInversions(state);
            return (inversionsCounter % 2 == 0);
        }

        private static int CountInversions(byte[,] state)
        {
            int inversionsCounter = 0;
            int singleArraySize = state.GetLength(0) * state.GetLength(1);
            int[] stateInSingleArray = new int[singleArraySize];
            int index = 0;
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    stateInSingleArray[index++] = state[i, j];
                }
            }

            for (int i = 0; i < singleArraySize - 1; i++)
            {
                for (int j = i + 1; j < singleArraySize; j++)
                {
                    if (stateInSingleArray[j] != 0 && stateInSingleArray[i] != 0 && stateInSingleArray[i] > stateInSingleArray[j])
                    {
                        inversionsCounter++;
                    }
                }
            }
            return inversionsCounter;
        }
        #endregion

        #region Expand
        public IList<byte[,]> Expand(byte[,] state)
        {
            List<byte[,]> possibleStates = new List<byte[,]>();

            var zeroPosition = FindCoordinatesOfElement(0, state);
            List<Point> possibleMoves = new List<Point>()
            {
                new Point(zeroPosition.X, zeroPosition.Y - 1),
                new Point(zeroPosition.X, zeroPosition.Y + 1),
                new Point(zeroPosition.X - 1, zeroPosition.Y),
                new Point(zeroPosition.X + 1, zeroPosition.Y)
            };

            foreach (var move in possibleMoves)
            {
                if (AreCooridinatesWithinTableRange(move.X, move.Y, state))
                {
                    possibleStates.Add(MakeMove(state, zeroPosition, move));
                }
            }

            return possibleStates;
        }       

        private byte[,] MakeMove(byte[,] state, Point zeroPosition, Point move)
        {
            byte[,] result = new byte[state.GetLength(0), state.GetLength(0)];

            for (byte i = 0; i < state.GetLength(0); i++)
            {
                for (byte j = 0; j < state.GetLength(1); j++)
                {
                    result[i, j] = state[i, j];
                }
            }
            result[zeroPosition.X, zeroPosition.Y] = state[move.X, move.Y];
            result[move.X, move.Y] = 0;

            return result;
        }

        private bool AreCooridinatesWithinTableRange(int x, int y, byte[,] state)
        {
            if (x < 0 || y < 0 || x >= state.GetLength(0) || y >= state.GetLength(1))
            {
                return false;
            }
            return true;
        }

        private Point FindCoordinatesOfElement(byte element, byte[,] state)
        {
            for (byte i = 0; i < state.GetLength(0); i++)
            {
                for (byte j = 0; j < state.GetLength(1); j++)
                {
                    if (state[i, j] == element)
                    {
                        return new Point(i, j);
                    }
                }
            }
            throw new ElementNotFoundInPrzesuwankaException($"Element {element} not found");
        }
        #endregion

        #region Comparing states
        public bool IsGoal(byte[,] state)
        {            
            return AreStatesTheSame(goal, state);
        }

        public bool AreStatesTheSame(byte[,] state1, byte[,] state2)
        {            
            for (byte i = 0; i < state1.GetLength(0); i++)
            {
                for (byte j = 0; j < state1.GetLength(1); j++)
                {
                    if (state1[i, j] != state2[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region Calculating cost
        private int ManhattanPriority(byte[,] state)
        {
            int manhattanPriority = 0;
            for (byte i = 0; i < state.GetLength(0); i++)
            {
                for (byte j = 0; j < state.GetLength(1); j++)
                {
                    if (state[i, j] != 0)
                    {
                        var cords = goalPositions[state[i, j]];
                        manhattanPriority += Math.Abs(cords.X - i) + Math.Abs(cords.Y - j);
                    }
                }
            }
            return manhattanPriority;
        }

        public int CompareStatesCost(byte[,] state1, byte[,] state2)
        {
            var costOfState1 = ManhattanPriority(state1);
            var costOfState2 = ManhattanPriority(state2);

            if (costOfState1 > costOfState2) return 1;
            if (costOfState1 < costOfState2) return -1;
            return 0;
        }
        #endregion
    }
}
