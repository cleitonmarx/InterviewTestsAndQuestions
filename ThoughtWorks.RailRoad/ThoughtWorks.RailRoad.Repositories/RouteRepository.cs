using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using ThoughtWorks.RailRoad.Domain.Locations;
using ThoughtWorks.RailRoad.Domain.Repositories;

namespace ThoughtWorks.RailRoad.Repositories
{
    /// <summary>
    /// Implementation of route repository interface
    /// </summary>
    public class RouteRepository : IRouteRepository
    {
        private readonly string _filePath;

        public RouteRepository()
        {
            _filePath = ConfigurationManager.AppSettings["RoutesNodesFilePath"];
           
        }

        public IEnumerable<RouteNode> GetAllRouteNodes()
        {
            var routeNodes = new List<RouteNode>();
            using (var stream = new StreamReader(_filePath))
            {
                var dataMatrix = stream.ReadToEnd().Split(',');
                routeNodes.AddRange(dataMatrix.Select(RouteNodeFactory.CreateRouteNode));
            }

            return routeNodes;
        }

        public IEnumerable<RouteNode> GetRouteNodesByCityName(string cityName)
        {
            return GetAllRouteNodes().Where(
                routeNode => routeNode.Origin.Name.Equals(cityName)
            );
        }

        public RouteNode GetRouteNodeByDestination(string originName, string destinationName)
        {
            return GetAllRouteNodes().FirstOrDefault(
                routeNode => routeNode.Origin.Name.Equals(originName) && 
                             routeNode.Destination.Name.Equals(destinationName)
            );
        }

    }
}
