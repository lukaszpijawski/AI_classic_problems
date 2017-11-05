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

    public class Przesuwanka : IProblem<byte[][]>
    {
        private byte[][] initial;
        private byte[][] goal;
         
        public Przesuwanka() : this(3)
        {
        }

        public Przesuwanka(int size) : this(MakeInitialState(size), MakeGoalState(size))
        {
            
        }        

        public Przesuwanka(byte[][] initial, byte[][] goal)
        {            
            this.initial = initial;
            this.goal = goal;
            PrintTable(initial);
            if (!IsSolvable(initial))
            {
                throw new UnsolvablePrzesuwankaException("Ta przesuwanka jest nierozwiązywalna");
            }
        }

        private static byte[][] MakeInitialState(int size)
        {
            if (size < 1)
            {
                throw new SizeOfPrzesuwankaLessThanOneException("Rozmiar przesuwanki musi wynosić co najmniej 1");
            }

            Random random = new Random();
            List<byte> setOfNumbers = new List<byte>();
            int numberOfElements = size * size;

            for (byte i = 0; i < numberOfElements; i++)
            {
                setOfNumbers.Add(i);
            }

            var initial = new byte[size][];
            for (byte i = 0; i < size; i++)
            {
                initial[i] = new byte[size];
                for (byte j = 0; j < size; j++)
                {
                    var index = (byte)random.Next(0, setOfNumbers.Count - 1);
                    var chosenNumber = setOfNumbers[index];
                    initial[i][j] = chosenNumber;
                    setOfNumbers.Remove(chosenNumber);
                }
            }
            return initial;
        }

        private static byte[][] MakeGoalState(int size)
        {
            if (size < 1)
            {
                throw new SizeOfPrzesuwankaLessThanOneException("Rozmiar przesuwanki musi wynosić co najmniej 1");
            }

            var goal = new byte[size][];
            byte filler = 0;
            for (byte i = 0; i < size; i++)
            {
                goal[i] = new byte[size];
                for (byte j = 0; j < size; j++)
                {
                    goal[i][j] = filler++;
                }
            }
            return goal;
        }

        private void PrintTable(byte[][] table)
        {
            foreach (var row in table)
            {
                Console.Write("| ");
                foreach (var column in row)
                {
                    Console.Write(column + " ");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine();
        }


        private bool IsSolvable(byte[][] state)
        {
            int inversionsCounter = CountInversions(state);
            return (inversionsCounter % 2 == 0);
        }

        private int CountInversions(byte[][] state)
        {
            int inversionsCounter = 0;
            int singleArraySize = state.GetLength(0) * state[0].Length;
            int[] stateInSingleArray = new int[singleArraySize];
            int index = 0;
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state[i].Length; j++)
                {
                    stateInSingleArray[index++] = state[i][j];
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

        

        public byte[][] InitialState
        {
            get
            {
                return initial;
            }
        }

        public IList<byte[][]> Expand(byte[][] state)
        {
            List<byte[][]> possibleStates = new List<byte[][]>();

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

        private byte[][] MakeMove(byte[][] state, Point zeroPosition, Point move)
        {
            byte[][] result = new byte[state.GetLength(0)][];

            for (byte i = 0; i < state.GetLength(0); i++)
            {
                result[i] = new byte[state[i].Length];
                for (byte j = 0; j < state[i].Length; j++)
                {
                    result[i][j] = state[i][j];
                }
            }
            result[zeroPosition.X][zeroPosition.Y] = state[move.X][move.Y];
            result[move.X][move.Y] = 0;

            return result;
        }

        private bool AreCooridinatesWithinTableRange(int x, int y, byte[][] state)
        {
            if (x < 0 || y < 0 || x >= state.GetLength(0) || y >= state[0].Length)
            {
                return false;
            }
            return true;
        }

        private Point FindCoordinatesOfElement(byte element, byte[][] state)
        {
            for (byte i = 0; i < state.GetLength(0); i++)
            {
                for (byte j = 0; j < state[i].Length; j++)
                {
                    if (state[i][j] == element)
                    {
                        return new Point(i, j);
                    }
                }
            }
            throw new ElementNotFoundInPrzesuwankaException($"Element {element} not found");
        }

        public bool IsGoal(byte[][] state)
        {            
            return AreStatesTheSame(goal, state);
        }

        public bool AreStatesTheSame(byte[][] state1, byte[][] state2)
        {            
            for (byte i = 0; i < state1.GetLength(0); i++)
            {
                for (byte j = 0; j < state1[i].Length; j++)
                {
                    if (state1[i][j] != state2[i][j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public int CompareStatesCost(byte[][] state1, byte[][] state2)
        {
            var inversionOfState1 = CountInversions(state1);
            var inversionOfState2 = CountInversions(state2);

            if (inversionOfState1 > inversionOfState2) return 1;
            if (inversionOfState1 < inversionOfState2) return -1;
            return 0;
        }
    }
}
