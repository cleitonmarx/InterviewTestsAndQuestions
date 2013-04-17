using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThoughtWorks.RailRoad.Domain.Exceptions;
using ThoughtWorks.RailRoad.Domain.Locations;

namespace ThoughtWorks.RailRoad.Domain.Tests
{
    [TestClass]
    public class RouteNodeTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_InputFromCityNull_ThrowArgumentNullException()
        {
            new RouteNode(null, null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_InputToCityNull_ThrowArgumentNullException()
        {
            new RouteNode(new City("A"), null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_InputDistanceEqualsZero_ThrowArgumentOutOfRangeException()
        {
            new RouteNode(new City("A"), new City("B"), 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRouteException))]
        public void Constructor_InputTwoEqualCities_ThrowInvalidRouteException()
        {
            new RouteNode(new City("A"), new City("A"), 2);
        }

        [TestMethod]
        public void Constructor_InputValidRoute_GetValidRoute()
        {
            var route = new RouteNode(new City("A"), new City("B"), 3);

            Assert.AreEqual("A", route.Origin.Name);
            Assert.AreEqual("B", route.Destination.Name);
            Assert.AreEqual(3, route.Distance);
        }
    }
}
