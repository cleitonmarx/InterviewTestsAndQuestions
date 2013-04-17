using System.Collections.Generic;
using System.Linq;
using ThoughtWorks.RailRoad.Domain.Locations;
using ThoughtWorks.RailRoad.Domain.Repositories;
using ThoughtWorks.RailRoad.Domain.Services;
using ThoughtWorks.RailRoad.Repositories;

namespace ThoughtWorks.RailRoad.UI.Application
{
    /// <summary>
    /// Facade that encapsulate the route calculation logic
    /// </summary>
    public static class RouteApplicationFacade
    {
        private static readonly IRouteRepository RouteRepository = new RouteRepository();

        /// <summary>
        /// Generate route by cities names
        /// </summary>
        /// <param name="citiNames">Cities names</param>
        /// <returns>Route</returns>
        public static Route GetRouteByCityNames(params string[] citiNames)
        {
            return GetRouteCalculatorService().GetRouteByCitiesNames(citiNames);
        }

        /// <summary>
        /// Generate routes by destination and number of stops
        /// </summary>
        /// <param name="originName">Name of origin city</param>
        /// <param name="destinationName">Name of destination city</param>
        /// <param name="minStops">Min stops</param>
        /// <param name="maxStops">Max stops</param>
        /// <returns>Routes</returns>
        public static IEnumerable<Route> GetRoutesByDestinationAndStops(
            string originName, string destinationName, int minStops,int maxStops
        )
        {
            var searchParameters = new SearchSpecificationGenerator()
                .WithDestination(originName, destinationName, true)
                .WithStops(minStops, maxStops);

            return  GetRouteCalculatorService().GetRoutesBySpecification(searchParameters.Generate());
        }

        /// <summary>
        /// Generate the shortest route between two cities
        /// </summary>
        /// <param name="originName">Name of origin city</param>
        /// <param name="destinationName">Name of destination city</param>
        /// <returns>Route</returns>
        public static Route GetShortestRouteByDestination(string originName, string destinationName)
        {
            var searchParameters = new SearchSpecificationGenerator().WithDestination(originName, destinationName, false);
            
            var routes = GetRouteCalculatorService().GetRoutesBySpecification(searchParameters.Generate());

            return routes.First(r => r.Distance == routes.Min(m => m.Distance));
        }

        /// <summary>
        /// Generate routes by destination and distanc
        /// </summary>
        /// <param name="originName">Name of origin city</param>
        /// <param name="destinationName">Name of destination city</param>
        /// <param name="minDistance">Max distance</param>
        /// <param name="maxDistance">Min distance</param>
        /// <returns>Routes</returns>
        public static IEnumerable<Route> GetRoutesByDestinationAndDistance(
            string originName, string destinationName, decimal minDistance, int maxDistance
        )
        {
            var searchParameters = new SearchSpecificationGenerator()
                .WithDestination(originName, destinationName, true)
                .WithDistance(minDistance, maxDistance);

            return GetRouteCalculatorService().GetRoutesBySpecification(searchParameters.Generate());
        }


        private static RouteCalculatorService GetRouteCalculatorService()
        {
            return new RouteCalculatorService(RouteRepository);
        }
    }
}
