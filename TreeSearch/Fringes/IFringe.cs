using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka
{
    public interface IFringe<Element>
    {
        void Add(Element element);
        bool IsEmpty { get; }
        Element Pop();
        void SetPriorityMethod(Func<Element, Element, int> compareMethod, Func<Element, Element, int> compareMethodWithPathCost);
        string GetName();
    }
}
