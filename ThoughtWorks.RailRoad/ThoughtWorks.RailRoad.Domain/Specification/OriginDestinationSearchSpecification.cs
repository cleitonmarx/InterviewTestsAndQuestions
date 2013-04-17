
using System;
using System.Linq;
using ThoughtWorks.RailRoad.Domain.Locations;

namespace ThoughtWorks.RailRoad.Domain.Specification
{
    /// <summary>
    /// A route search specification that check if a origin and destination was reached
    /// </summary>
    public class OriginDestinationSearchSpecification : IRouteSearchSpecification<Route>
    {
        private readonly City _origin;
        private readonly City _destination;
        private readonly bool _keepSearching;

        public OriginDestinationSearchSpecification(City origin, City destination, bool keepSearching)
        {
            if (origin == null) throw new ArgumentNullException("origin");
            if (destination == null) throw new ArgumentNullException("destination");

            _origin = origin;
            _destination = destination;
            _keepSearching = keepSearching;
        }
        public bool IsSearchSatisfiedBy(Route entity)
        {
            return entity.Origin.Name == _origin.Name && entity.Destination.Name == _destination.Name;
        }

        public bool CanKeepSearching(Route entity)
        {
            return entity.Origin.Name == _origin.Name && GetVerification(entity);
        }

        private bool GetVerification(Route entity)
        {
            var result = true;

            if (!_keepSearching)
            {
                result = entity.Nodes.All(node => entity.Nodes.Count(n => n.Destination.Name == node.Destination.Name &&
                                                                          n.Origin.Name == node.Origin.Name) <= 1);
            }

            return result;
        }
    }
}
