using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka
{
    public class PriorityAStarFringe<Element> : PriorityQueueFringe<Element>
    {
        public override string GetName()
        {
            return "PriorityAStarFringe";
        }

        public override void SetPriorityMethod(Func<Element, Element, int> compareMethod, Func<Element, Element, int> compareMethodWithPathCost)
        {
            this.compareElementsPriority = compareMethodWithPathCost;
        }        
    }
}
