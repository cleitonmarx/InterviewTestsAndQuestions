using System;
using ThoughtWorks.RailRoad.Domain.Locations;

namespace ThoughtWorks.RailRoad.Domain.Specification
{
    /// <summary>
    /// A route search specification that check the number of stops of a route
    /// </summary>
    public class StopSearchSpecification : IRouteSearchSpecification<Route>
    {
        private readonly int _minStops;
        private readonly int _maxStops;

        public StopSearchSpecification(int minStops, int maxStops)
        {
            if (minStops <= 0) throw new ArgumentOutOfRangeException("minStops", "Stops must be greater than 0");
            if (maxStops <= 0) throw new ArgumentOutOfRangeException("maxStops", "Stops must be greater than 0");
            if (maxStops < minStops) throw new ArgumentException("The max stops must be greater than min stops.");

            _minStops = minStops;
            _maxStops = maxStops;
        }
        public bool IsSearchSatisfiedBy(Route entity)
        {
            return entity.Stops >= _minStops && entity.Stops <= _maxStops;
        }

        public bool CanKeepSearching(Route entity)
        {
            return entity.Stops <= _maxStops;
        }
    }
}
