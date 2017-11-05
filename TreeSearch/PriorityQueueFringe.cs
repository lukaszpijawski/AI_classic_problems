using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka
{
    public class PriorityQueueFringe<Element> : IFringe<Element>
    {
        private Func<Element, Element, int> compareElements = null;
        private List<Element> heap = new List<Element>();
       
        private void Heapify(List<Element> heap, int index, int heapSize)
        {
            int l, r, largest;
            Element x;
            Func<int, int> left = a => 2 * a + 1;
            Func<int, int> right = a => 2 * a + 2;
            Func<Element, Element, bool> isSmaller = (el1, el2) =>
            {
                return this.compareElements(el1, el2) == -1 ? true : false;
            };

            l = left(index);
            r = right(index);

            largest = (l < heapSize && isSmaller(heap[l], heap[index])) ? l : index;
            largest = (r < heapSize && isSmaller(heap[r], heap[largest])) ? r : largest;

            if (largest != index)
            {
                x = heap[index];
                heap[index] = heap[largest];
                heap[largest] = x;
                Heapify(heap, largest, heapSize);
            }
        }

        private void BuildHeap(List<Element> heap, int heapSize)
        {
            for (int i = (heapSize - 1) / 2; i >= 0; i--)
            {
                Heapify(heap, i, heapSize);
            }
        }
       
        public bool IsEmpty
        {
            get
            {
                return this.heap.Count == 0;
            }
        }

        public Element Pop()
        {
            var element = this.heap[0];
            this.heap.RemoveAt(0);
            return element;
        }

        public void Add(Element element, Func<Element, Element, int> compareElements)
        {
            if (this.compareElements == null)
            {
                this.compareElements = compareElements;                
            }
            this.heap.Add(element);
            BuildHeap(this.heap, heap.Count);
        }
    }
}
