using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThoughtWorks.RailRoad.Domain.Locations;
using ThoughtWorks.RailRoad.Domain.Specification;

namespace ThoughtWorks.RailRoad.Domain.Tests
{
    [TestClass]
    public class OriginDestinationSearchSpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullOriginCity_ThrowsArgumentNullException()
        {
            new OriginDestinationSearchSpecification(null, new City("A"), true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullDestinationCity_ThrowsArgumentNullException()
        {
            new OriginDestinationSearchSpecification(new City("A"), null, true);
        }
    }
}
