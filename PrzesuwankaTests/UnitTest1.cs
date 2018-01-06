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
        public void TestCalculateStraightLineDistance()
        {
            //arrange
            City city1 = new City("A", new Point(2, 1));
            City city2 = new City("B", new Point(6, 4));
            PrivateObject romaniaMap = new PrivateObject(new RomaniaMap(city1, city2, new System.Collections.Generic.List<City>()));

            //act
            var result = romaniaMap.Invoke("CalculateStraightLineDistance", city1, city2);

            //assert
            Assert.AreEqual(result, 5.0);
        }
    }
}
