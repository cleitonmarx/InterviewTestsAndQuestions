using System.Collections.Generic;
using ThoughtWorks.RailRoad.Domain;
using ThoughtWorks.RailRoad.Domain.Locations;

namespace ThoughtWorks.RailRoad.Domain.Repositories
{
    /// <summary>
    /// Represent a contract for a data repository of route nodes.
    /// </summary>
    public interface IRouteRepository
    {
        /// <summary>
        /// Get all routes nodes of railroad 
        /// </summary>
        /// <returns>list of a route nodes</returns>
        IEnumerable<RouteNode> GetAllRouteNodes();

        /// <summary>
        /// Get all route nodes of a specific city.
        /// </summary>
        /// <param name="cityName">City name</param>
        /// <returns></returns>
        IEnumerable<RouteNode> GetRouteNodesByCityName(string cityName);

        /// <summary>
        /// Get a specific route node between two cities.
        /// </summary>
        /// <param name="originName">Origin city name</param>
        /// <param name="destinationName">Destination city name</param>
        /// <returns></returns>
        RouteNode GetRouteNodeByDestination(string originName, string destinationName);
    }
}
