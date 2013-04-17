using System.Collections.Generic;
using System.Linq;

namespace ThoughtWorks.RailRoad.Domain.Specification
{
    /// <summary>
    /// A route search specification that is the AND of others specifications.
    /// </summary>
    public class AndSearchSpecification<T> : IRouteSearchSpecification<T>
    {
        private readonly IEnumerable<IRouteSearchSpecification<T>> _listRouteSearchSpecification;
 
        public AndSearchSpecification(params IRouteSearchSpecification<T>[] routeSearchSpecifications)
        {
            _listRouteSearchSpecification = routeSearchSpecifications;
        }
       

        public bool IsSearchSatisfiedBy(T entity)
        {
            return _listRouteSearchSpecification.All(specification => specification.IsSearchSatisfiedBy(entity));
        }


        public bool CanKeepSearching(T entity)
        {
            return _listRouteSearchSpecification.All(specification => specification.CanKeepSearching(entity));
        }
    }
}
