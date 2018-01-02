using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka
{
    class StackFringe<Element> : IFringe<Element>
    {
        private Stack<Element> stack = new Stack<Element>();

        public bool IsEmpty
        {
            get
            {
                return !stack.Any();
            }
        }

        public void Add(Element element)
        {
            stack.Push(element);
        }

        public string GetName()
        {
            return "StackFringe";
        }

        public Element Pop()
        {
            return stack.Pop();
        }

        public void SetPriorityMethod(Func<Element, Element, int> compareMethod, Func<Element, Element, int> compareMethodWithPathCost)
        {            
        }
    }
}
