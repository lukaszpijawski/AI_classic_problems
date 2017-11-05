using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka
{
    public interface IProblem<State>
    {
        State InitialState { get; }
        bool IsGoal(State state);
        IList<State> Expand(State state);
        bool AreStatesTheSame(State state1, State state2);
        int CompareStatesCost(State state1, State state2);
    }
}
