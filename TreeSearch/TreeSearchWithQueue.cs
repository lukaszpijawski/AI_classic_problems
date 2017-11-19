﻿using System;
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
            Func<Node<State>, Node<State>, int> compareStatesPriority = (node1, node2) =>
            {
                return problem.CompareStatesPriority(node1.NodeState, node1.PathCost, node2.NodeState, node2.PathCost);
            };

            fringe.Add(new Node<State>(problem.InitialState, null, 0.0), compareStatesPriority);
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
                        var newNode = new Node<State>(state, node, node.PathCost + problem.CalculateCostToNextState(state, node.NodeState));
                        fringe.Add(newNode, compareStatesPriority);
                    }
                }
            }
            return null;
        }
    }
}
