using System;
using ThoughtWorks.RailRoad.Domain.Exceptions;

namespace ThoughtWorks.RailRoad.Domain.Locations
{
    /// <summary>
    /// Represents a node of a route
    /// </summary>
    public class RouteNode
    {
        public RouteNode(City origin, City destination, decimal distance)
        {
            if (origin == null) throw new ArgumentNullException("origin");
            if (destination == null) throw new ArgumentNullException("destination");
            if (destination.Name == origin.Name) throw new InvalidRouteException("Cities must be different");
            if (distance <= 0) throw new ArgumentOutOfRangeException("distance", "distance must be greater than 0.");

            Origin = origin;
            Destination = destination;
            Distance = distance;
        }
        public City Origin { get; private set; }
        public City Destination { get; private set; }
        public decimal Distance { get; private set; }
    }
}
