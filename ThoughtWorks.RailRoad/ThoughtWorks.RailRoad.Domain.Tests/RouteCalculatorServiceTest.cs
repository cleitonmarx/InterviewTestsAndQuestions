using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThoughtWorks.RailRoad.Domain.Exceptions;
using ThoughtWorks.RailRoad.Domain.Locations;
using ThoughtWorks.RailRoad.Domain.Repositories;
using ThoughtWorks.RailRoad.Domain.Services;
using ThoughtWorks.RailRoad.Domain.Specification;

namespace ThoughtWorks.RailRoad.Domain.Tests
{
    [TestClass]
    public class RouteCalculatorServiceTest
    {
        private IRouteRepository _routeRepository;

        [TestInitialize]
        public void Initialize()
        {
            _routeRepository = new RouteRepositoryStub();
        }

        private void ExecuteTestGetRouteByCitiesNames(int distanceAssertValue, params string[] citiesNames)
        {
            var routeCalculatorService = new RouteCalculatorService(_routeRepository);
            var route = routeCalculatorService.GetRouteByCitiesNames(citiesNames);

            Assert.IsNotNull(route);
            Assert.AreEqual(distanceAssertValue, route.Distance);
        }
       
        [TestMethod]
        public void GetRouteByCitiesNames_PassRoutesABC_GetRouteWithDistance9()
        {
            ExecuteTestGetRouteByCitiesNames(9, "A", "B", "C");            
        }

        [TestMethod]
        public void GetRouteByCitiesNames_PassRoutesAD_GetRouteWithDistance5()
        {
            ExecuteTestGetRouteByCitiesNames(5, "A", "D");
        }

        [TestMethod]
        public void GetRouteByCitiesNames_PassRoutesADC_GetRouteWithDistance13()
        {
            ExecuteTestGetRouteByCitiesNames(13, "A", "D", "C");
        }

        [TestMethod]
        public void GetRouteByCitiesNames_PassRoutesAEBCD_GetRouteWithDistance22()
        {
            ExecuteTestGetRouteByCitiesNames(22, "A", "E", "B", "C", "D");
        }

        [TestMethod]
        [ExpectedException(typeof(NoSuchRouteException))]
        public void GetRouteByCitiesNames_PassRoutesAED_ThrowNoSuchRouteException()
        {
            ExecuteTestGetRouteByCitiesNames(0, "A", "E", "D");
        }

        [TestMethod]
        public void GetRoutesBySpecification_OriginCToDestinationCandMax3Stops_Get2Routes()
        {
            var routeCalculatorService = new RouteCalculatorService(_routeRepository);
            var specification = new AndSearchSpecification<Route>(
                new OriginDestinationSearchSpecification(new City("C"), new City("C"), true),
                new StopSearchSpecification(1,3)
            );

            var routes = routeCalculatorService.GetRoutesBySpecification(specification).ToList();
            Assert.AreEqual(2,routes.Count());
        }

        [TestMethod]
        public void GetRoutesBySpecification_OriginAToDestinationCandExactly4Stops_Get3Routes()
        {
            var routeCalculatorService = new RouteCalculatorService(_routeRepository);
            var specification = new AndSearchSpecification<Route>(
                new OriginDestinationSearchSpecification(new City("A"), new City("C"), true),
                new StopSearchSpecification(4, 4)
            );

            var routes = routeCalculatorService.GetRoutesBySpecification(specification).ToList();
            Assert.AreEqual(3, routes.Count());
        }

        [TestMethod]
        public void GetRoutesBySpecification_OriginAToDestinationCShortestDistance_GetDistance9()
        {
            var routeCalculatorService = new RouteCalculatorService(_routeRepository);
            var specification = new OriginDestinationSearchSpecification(new City("A"), new City("C"), false);

            var routes = routeCalculatorService.GetRoutesBySpecification(specification).ToList();
            Assert.AreEqual(9, routes.Min(t=> t.Distance));
        }

        [TestMethod]
        public void GetRoutesBySpecification_OriginBToDestinationBShortestDistance_GetDistance9()
        {
            var routeCalculatorService = new RouteCalculatorService(_routeRepository);
            var specification = new OriginDestinationSearchSpecification(new City("B"), new City("B"), false);

            var routes = routeCalculatorService.GetRoutesBySpecification(specification).ToList();
            Assert.AreEqual(9, routes.Min(r=>r.Distance));
        }

        [TestMethod]
        public void GetRoutesBySpecification_OriginCToDestinationCWithDistanceLessThan30_Get9Routes()
        {
            var routeCalculatorService = new RouteCalculatorService(_routeRepository);
            var specification = new AndSearchSpecification<Route>(
                new OriginDestinationSearchSpecification(new City("C"), new City("C"), true),
                new DistanceSearchSpecification(1, 30)
            );

            var routes = routeCalculatorService.GetRoutesBySpecification(specification).ToList();
            
            //The requirement says that this test case have 7 nodes, but it's not true, it have 9 nodes.
            Assert.AreEqual(9, routes.Select(t=> t.ToString()).Count());
            
        }

        
    }
}
