using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka
{
    public interface IFringe<Element>
    {
        void Add(Element element, Func<Element, Element, int> compareElements = null);
        bool IsEmpty { get; }
        Element Pop();
    }
}
