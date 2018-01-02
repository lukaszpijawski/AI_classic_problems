using Przesuwanka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka
{
    public class QueueFringe<Element> : IFringe<Element>
    {
        Queue<Element> queue = new Queue<Element>();

        public bool IsEmpty
        {
            get
            {
                return !queue.Any();
            }
        }

        public string GetName()
        {
            return "QueueFringe";
        }

        public void Add(Element element)
        {
            queue.Enqueue(element);
        }

        public Element Pop()
        {
            return queue.Dequeue();
        }

        public void SetPriorityMethod(Func<Element, Element, int> compareMethod, Func<Element, Element, int> compareMethodWithPathCost)
        {            
        }
    }
}
