using System.Collections.Generic;
using System.Linq;
using ThoughtWorks.RailRoad.Domain.Locations;
using ThoughtWorks.RailRoad.Domain.Repositories;

namespace ThoughtWorks.RailRoad.Domain.Tests
{
    public class RouteRepositoryStub : IRouteRepository
    {
        private readonly IEnumerable<RouteNode> _routeNodesInSystem; 
        public RouteRepositoryStub()
        {
            _routeNodesInSystem = new List<RouteNode>
            {
                new RouteNode(new City("A"), new City("B"), 5),//AB5
                new RouteNode(new City("B"), new City("C"), 4),//BC4
                new RouteNode(new City("C"), new City("D"), 8),//CD8
                new RouteNode(new City("D"), new City("C"), 8),//DC8
                new RouteNode(new City("D"), new City("E"), 6),//DE6
                new RouteNode(new City("A"), new City("D"), 5),//AD5
                new RouteNode(new City("C"), new City("E"), 2),//CE2
                new RouteNode(new City("E"), new City("B"), 3),//EB3
                new RouteNode(new City("A"), new City("E"), 7),//AE7
            };
        }

        public IEnumerable<RouteNode> GetAllRouteNodes()
        {
            return _routeNodesInSystem;
        }

        public IEnumerable<RouteNode> GetRouteNodesByCityName(string cityName)
        {
            return _routeNodesInSystem.Where(
                routeNode => routeNode.Origin.Name.Equals(cityName)
            );
        }

        public RouteNode GetRouteNodeByDestination(string originName, string destinationName)
        {
            return _routeNodesInSystem.FirstOrDefault(
                routeNode => routeNode.Origin.Name.Equals(originName) && 
                             routeNode.Destination.Name.Equals(destinationName)
            );
        }
    }
}
