using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka
{
    public class PriorityQueueFringe<Element> : IFringe<Element>        
    {
        private Func<Element, Element, int> CompareElementsPriority = null;
        private List<Element> heap = new List<Element>();

        //private void MinHeapify(List<Element> heap, int index, int heapSize)
        //{
        //    int left, right, smallest;
        //    Element x;
        //    Func<int, int> leftNode = a => 2 * a + 1;
        //    Func<int, int> rightNode = a => 2 * a + 2;
            
        //    left = leftNode(index);
        //    right = rightNode(index);

        //    smallest = (left < heapSize && HasFirstElementHigherPriority(heap[left], heap[index])) ? left : index;
        //    smallest = (right < heapSize && HasFirstElementHigherPriority(heap[right], heap[smallest])) ? right : smallest;

        //    if (smallest != index)
        //    {
        //        x = heap[index];
        //        heap[index] = heap[smallest];
        //        heap[smallest] = x;
        //        MinHeapify(heap, smallest, heapSize);
        //    }
        //}

        //private void BuildHeap(List<Element> heap, int heapSize)
        //{
        //    for (int i = (heapSize - 1) / 2; i >= 0; i--)
        //    {
        //        MinHeapify(heap, i, heapSize);
        //    }
        //}
       
        public bool IsEmpty
        {
            get
            {
                return !this.heap.Any();
            }
        }

        public Element Pop()
        {
            var element = this.heap[0];
            this.heap.RemoveAt(0);
            return element;
        }

        public void Add(Element element, Func<Element, Element, int> compareElementsPriority)
        {
            if (this.CompareElementsPriority == null)
            {
                this.CompareElementsPriority = compareElementsPriority;
            }
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
            return this.CompareElementsPriority(element1, element2) == 1 ? true : false;
        }
    }
}
