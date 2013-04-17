using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThoughtWorks.RailRoad.Domain.Specification;

namespace ThoughtWorks.RailRoad.Domain.Tests
{
    [TestClass]
    public class DistanceSearchSpecificationTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_WithMaxDistanceLessThan0_ThrowArgumentOutOfRangeException()
        {
            new DistanceSearchSpecification(1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_WithMinDistanceLessThan0_ThrowArgumentOutOfRangeException()
        {
            new DistanceSearchSpecification(0, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithMinDistanceGreaterThanMaxDistance_ThrowArgumentException()
        {
            new DistanceSearchSpecification(3, 1);
        }
    }
}
