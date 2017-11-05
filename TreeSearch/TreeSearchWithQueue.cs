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
            Node<State>.CompareStatesCost = problem.CompareStatesCost;
            fringe.Add(new Node<State>(problem.InitialState, null), Node<State>.CompareNodesCost);
            while (!fringe.IsEmpty)
            {
                var node = fringe.Pop();
                if (problem.IsGoal(node.NodeState))
                {
                    return node;
                }
                foreach (var state in problem.Expand(node.NodeState))
                {
                    if (!node.OnPathToRoot(state, problem.AreStatesTheSame))
                    {
                        fringe.Add(new Node<State>(state, node), Node<State>.CompareNodesCost);
                    }
                }
            }
            return null;
        }
    }
}
