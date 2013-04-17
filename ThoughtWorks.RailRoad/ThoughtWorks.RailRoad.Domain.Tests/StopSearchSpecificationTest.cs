using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThoughtWorks.RailRoad.Domain.Specification;

namespace ThoughtWorks.RailRoad.Domain.Tests
{
    [TestClass]
    public class StopSearchSpecificationTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_WithMaxDistanceLessThan0_ThrowArgumentOutOfRangeException()
        {
            new StopSearchSpecification(1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_WithMinDistanceLessThan0_ThrowArgumentOutOfRangeException()
        {
            new StopSearchSpecification(0, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithMinStopsGreaterThanMaxStops_ThrowArgumentException()
        {
            new StopSearchSpecification(3, 1);
        }
    }
}
