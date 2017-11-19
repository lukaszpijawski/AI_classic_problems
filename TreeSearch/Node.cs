using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka
{
    public class Node<State>
    {
        public State NodeState { get; private set; }
        private Node<State> parent;
        public double PathCost { get; private set; }

        public Node(State state, Node<State> parent, double pathCost)
        {
            this.NodeState = state;
            this.parent = parent;
            this.PathCost = pathCost;
        }

        public bool OnPathToRoot(State state, Func<State, State, bool> areStatesTheSame)
        {
            if (areStatesTheSame(this.NodeState, state))
            {
                return true;
            }
            if (parent == null)
            {
                return false; 
            }
            return OnPathToRoot(parent, state, areStatesTheSame);
        }

        private bool OnPathToRoot(Node<State> node, State state, Func<State, State, bool> areStatesTheSame)
        {            
            if (areStatesTheSame(node.NodeState, state))
            {
                return true;
            }
            if (node.parent == null)
            {
                return false;
            }
            return OnPathToRoot(node.parent, state, areStatesTheSame);
        }
    }
}
