using System.Collections.Generic;
using ThoughtWorks.RailRoad.Domain.Exceptions;
using ThoughtWorks.RailRoad.Domain.Locations;
using ThoughtWorks.RailRoad.Domain.Repositories;
using ThoughtWorks.RailRoad.Domain.Specification;

namespace ThoughtWorks.RailRoad.Domain.Services
{
    /// <summary>
    /// Domain service that realize route calculations
    /// </summary>
    public class RouteCalculatorService
    {
        private readonly IRouteRepository _routeRepository;

        public RouteCalculatorService(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        /// <summary>
        /// Get routes by a route search specification
        /// </summary>
        /// <param name="routeSearchSpecification">A route search specification</param>
        /// <returns>A list of routes</returns>
        public IEnumerable<Route> GetRoutesBySpecification(IRouteSearchSpecification<Route> routeSearchSpecification)
        {
            var routeNodes = _routeRepository.GetAllRouteNodes();
            foreach (var node in routeNodes)
            {
                var route = new Route {Origin = node.Origin, Destination = node.Destination};
                route.AddNode(node);
                var routes = SearchRoutes(route, routeSearchSpecification);
                foreach (var routeFound in routes)
                {
                    yield return routeFound;
                }
            }

        }

        /// <summary>
        /// Get a route of a list of cities
        /// </summary>
        /// <param name="citiesNames">A list of cities names</param>
        /// <returns>A route</returns>
        public Route GetRouteByCitiesNames(params string[] citiesNames)
        {
            var route = new Route
            {
                Origin = new City(citiesNames[0]),
                Destination = new City(citiesNames[citiesNames.Length - 1])
            };

            for (var i = 0; i < citiesNames.Length-1; i++)
            {
                var routeNode = _routeRepository.GetRouteNodeByDestination(citiesNames[i], citiesNames[i + 1]);
                if (routeNode == null) throw new NoSuchRouteException();
                route.AddNode(routeNode);
            }

            return route;
        }

        /// <summary>
        /// Recursive method that scan a route by a search specification
        /// </summary>
        /// <param name="route">A route with origin and destinations informations to find their nodes</param>
        /// <param name="routeSearchSpecification">A search specification</param>
        /// <returns>A list of routes</returns>
        private IEnumerable<Route> SearchRoutes(Route route, IRouteSearchSpecification<Route> routeSearchSpecification)
        {
            if (!routeSearchSpecification.CanKeepSearching(route)) yield break;
            if (routeSearchSpecification.IsSearchSatisfiedBy(route)) yield return route;

            var routeNodes = _routeRepository.GetRouteNodesByCityName(route.Destination.Name);
            foreach (var node in routeNodes)
            {
                var newRoute = new Route(new List<RouteNode>(route.Nodes))
                {
                    Origin = route.Origin,
                    Destination = node.Destination
                };

                newRoute.AddNode(node);
                foreach (var routeFound in SearchRoutes(newRoute, routeSearchSpecification))
                {
                    yield return routeFound;
                }
            }
        }
    }
}
