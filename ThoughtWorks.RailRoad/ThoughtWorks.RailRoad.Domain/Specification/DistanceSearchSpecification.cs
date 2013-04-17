using System;
using ThoughtWorks.RailRoad.Domain.Locations;

namespace ThoughtWorks.RailRoad.Domain.Specification
{
    /// <summary>
    /// A route search specification that check the distance of a route
    /// </summary>
    public class DistanceSearchSpecification : IRouteSearchSpecification<Route>
    {
        private readonly decimal _minDistance;
        private readonly decimal _maxDistance;

        public DistanceSearchSpecification(decimal minDistance, decimal maxDistance)
        {
            if (minDistance <= 0) throw new ArgumentOutOfRangeException("minDistance", "Distances must be greater than zero.");
            if (maxDistance <= 0) throw new ArgumentOutOfRangeException("maxDistance", "Distances must be greater than zero.");
            if (maxDistance < minDistance) throw new ArgumentException("The max distance must be greater than min distance.");
            _minDistance = minDistance;
            _maxDistance = maxDistance;
        }
        public bool IsSearchSatisfiedBy(Route entity)
        {
            return entity.Distance >= _minDistance && entity.Distance <= _maxDistance;
            
        }

        public bool CanKeepSearching(Route entity)
        {
            return entity.Distance <= _maxDistance;
        }
    }
}
