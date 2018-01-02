using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka
{
    public class PriorityAStarFringe<Element> : IFringe<Element>
    {
        protected Func<Element, Element, int> compareElementsPriority = null;
        private List<Element> heap = new List<Element>();

        public bool IsEmpty
        {
            get
            {
                return !this.heap.Any();
            }
        }


        public string GetName()
        {
            return "PriorityAStarFringe";
        }

        public Element Pop()
        {
            var element = this.heap[0];
            this.heap.RemoveAt(0);
            return element;
        }

        public void Add(Element element)
        {
            this.heap.Add(element);
            RepairHeap(heap.Count - 1);
        }

        private void RepairHeap(int nodeIndex)
        {
            int parentIndex;
            Element tmp;
            Func<int, int> parent = a => (a - 1) / 2;
            if (nodeIndex != 0)
            {
                parentIndex = parent(nodeIndex);
                if (HasFirstElementHigherPriority(heap[nodeIndex], heap[parentIndex]))
                {
                    tmp = heap[parentIndex];
                    heap[parentIndex] = heap[nodeIndex];
                    heap[nodeIndex] = tmp;
                    RepairHeap(parentIndex);
                }
            }
        }

        private bool HasFirstElementHigherPriority(Element element1, Element element2)
        {
            return this.compareElementsPriority(element1, element2) == 1 ? true : false;
        }

        public void SetPriorityMethod(Func<Element, Element, int> compareMethod, Func<Element, Element, int> compareMethodWithPathCost)
        {
            this.compareElementsPriority = compareMethodWithPathCost;
        }        
    }
}
