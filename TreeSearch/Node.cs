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

        public Node(State state, Node<State> parent)
        {
            this.NodeState = state;
            this.parent = parent;
        }

        public bool OnPathToRoot(State state, Func<State, State, bool> areStatesTheSame)
        {
            bool onPathToRoot = false;
            if (areStatesTheSame(this.NodeState, state))
            {
                return true;
            }
            if (parent != null)
            {
                onPathToRoot = OnPathToRoot(parent, state, areStatesTheSame);
            }
            return onPathToRoot;
        }

        private bool OnPathToRoot(Node<State> node, State state, Func<State, State, bool> areStatesTheSame)
        {
            bool onPathToRoot = false;
            if (areStatesTheSame(node.NodeState, state))
            {
                return true;
            }
            if (node.parent != null)
            {
                onPathToRoot = OnPathToRoot(node.parent, state, areStatesTheSame);
            }
            return onPathToRoot;
        }
    }
}
