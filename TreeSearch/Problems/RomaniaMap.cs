using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka
{
    public class RomaniaMap : IProblem<City>
    {
        #region Fields
        private City _initial;
        private City _goal;
        private List<City> _cities;
        #endregion

        #region Constructors       
        public RomaniaMap(City initial, City goal, List<City> cities)
        {
            _initial = initial;
            _goal = goal;
            _cities = cities;            
        }
        #endregion

        #region PrintCities
        public static void PrintCities(List<City> cities)
        {
            foreach (var city in cities)
            {
                Console.WriteLine(city.Name + ", neighbours:");
                foreach (var neighbour in city.Neighbours)
                {
                    Console.WriteLine("\t{0} - {1}", neighbour.Name, neighbour.Distance);
                }
            }
        }        
        #endregion

        #region Properties
        public City InitialState
        {
            get
            {
                return _initial;
            }
        }
        #endregion

        #region Expand
        public IList<City> Expand(City state)
        {
            var possibleStates = new List<City>();
            foreach (var neighbour in state.Neighbours)
            {
                possibleStates.Add(neighbour.City);
            }
            return possibleStates;
        }
        #endregion

        #region Comparing states
        public bool IsGoal(City state)
        {
            return state.Name == _goal.Name;
        }

        public bool AreStatesTheSame(City state1, City state2)
        {
            return state1.Name == state2.Name;
        }
        #endregion

        #region Calculating cost
        public int CompareStatesPriority(City state1, City state2)
        {
            var costOfState1 = CalculateCostToGoal(state1);
            var costOfState2 = CalculateCostToGoal(state2);

            if (costOfState1 > costOfState2) return -1;
            if (costOfState1 < costOfState2) return 1;
            return 0;
        }

        public int CompareStatesPriorityWithPathCost(City state1, City state2, double state1PathCost, double state2PathCost)
        {
            var costOfState1 = CalculateCostToGoal(state1) + state1PathCost;
            var costOfState2 = CalculateCostToGoal(state2) + state2PathCost;

            if (costOfState1 > costOfState2) return -1;
            if (costOfState1 < costOfState2) return 1;
            return 0;
        }

        public double CalculateCostToNextState(City state1, City state2)
        {
            return state1.Neighbours.Find(a => a.Name == state2.Name).Distance;
        }

        public double CalculateCostToGoal(City state)
        {
            return CalculateStraightLineDistance(state, _goal);
        }

        private double CalculateStraightLineDistance(City city1, City city2)
        {
            return Math.Sqrt(Math.Pow(city1.Coordinates.X - city2.Coordinates.X, 2) + Math.Pow(city1.Coordinates.Y - city2.Coordinates.Y, 2));
        }
        #endregion
    }
}
