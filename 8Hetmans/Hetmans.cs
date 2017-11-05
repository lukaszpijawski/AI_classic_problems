using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przesuwanka;

namespace Przesuwanka
{
    public class Hetmans : IProblem<byte[]>
    {
        private byte[] initial;
        private byte[] goal;
         
        public byte[] InitialState => throw new NotImplementedException();

        public bool AreStatesTheSame(byte[] state1, byte[] state2)
        {
            throw new NotImplementedException();
        }

        public IList<byte[]> Expand(byte[] state)
        {
            throw new NotImplementedException();
        }

        public bool IsGoal(byte[] state)
        {
            throw new NotImplementedException();
        }
    }
}
