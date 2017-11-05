using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Przesuwanka;

namespace PrzesuwankaTests
{
    [TestClass]
    public class UnitTest1
    {     

        [TestMethod]
        public void TestContstructorMethod()
        {
            byte[][] initial = new byte[3][] { new byte[] { 1, 0, 2 }, new byte[] { 3, 4, 5 }, new byte[] { 6, 7, 8 } };
            byte[][] goal = new byte[3][] { new byte[] { 0, 1, 2 }, new byte[] { 3, 4, 5 }, new byte[] { 6, 7, 8 } };

            Przesuwanka.Przesuwanka przesuwanka = new Przesuwanka.Przesuwanka();
        }

        [TestMethod]
        public void TestExpandMethod()
        {
            
        }
    }
}
