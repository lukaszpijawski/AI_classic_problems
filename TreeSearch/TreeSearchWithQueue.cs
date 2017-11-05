using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka
{
    public static class TreeSearchWithQueue<State>
    {
        public static Node<State> Search(IProblem<State> problem, IFringe<Node<State>> fringe)
        {
            //StateComparer<State> stateComparer = new StateComparer<State>(problem.AreStatesTheSame);
            //HashSet<State> hashSet = new HashSet<State>();
            Func<Node<State>, Node<State>, int> compareStatesCost = (state1, state2) =>
            {
                return problem.CompareStatesCost(state1.NodeState, state2.NodeState);
            };

            fringe.Add(new Node<State>(problem.InitialState, null), compareStatesCost);            
            while (!fringe.IsEmpty)
            {
                var node = fringe.Pop();
                //hashSet.Add(node.NodeState);
                if (problem.IsGoal(node.NodeState))
                {
                    return node;
                }
                foreach (var state in problem.Expand(node.NodeState))
                {
                    if (!node.OnPathToRoot(state, problem.AreStatesTheSame /*!hashSet.Contains(state, stateComparer)*/))
                    {
                        fringe.Add(new Node<State>(state, node), compareStatesCost);
                    }
                }
            }
            return null;
        }
    }

    public class StateComparer<State> : EqualityComparer<State>
    {
        private Func<State, State, bool> areStatesTheSame;
        public StateComparer(Func<State, State, bool> areStatesTheSame)
        {
            this.areStatesTheSame = areStatesTheSame;
        }

        public override bool Equals(State x, State y)
        {
            return this.areStatesTheSame(x, y);
        }

        public override int GetHashCode(State obj)
        {
            return 1;
        }
    }
}
