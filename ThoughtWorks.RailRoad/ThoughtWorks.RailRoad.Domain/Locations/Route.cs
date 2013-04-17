using System.Collections.Generic;
using System.Linq;

namespace ThoughtWorks.RailRoad.Domain.Locations
{
    /// <summary>
    /// Represents a route between two cities.
    /// </summary>
    public class Route
    {
        public Route()
        {
            Nodes = new List<RouteNode>();
        }

        public Route(IEnumerable<RouteNode> nodes )
        {
            Nodes = nodes;
        }
        public IEnumerable<RouteNode> Nodes { get; private set; }
        public City Origin { get; set; }
        public City Destination { get; set; }

        public decimal Distance
        {
            get { return Nodes.Sum(route => route.Distance); }
        }

        public int Stops
        {
            get { return Nodes.Count(); }
        }

        public void AddNode(RouteNode node)
        {
            var list = Nodes as List<RouteNode>;
            if (list != null) list.Add(node);
        }

        public override string ToString()
        {
            return Origin.Name + Nodes.Select(r => r.Destination.Name)
                                       .Aggregate((current, next) => current + next);
        }
    }
}
