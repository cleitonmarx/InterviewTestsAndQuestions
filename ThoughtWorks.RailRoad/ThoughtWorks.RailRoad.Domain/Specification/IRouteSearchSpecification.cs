namespace ThoughtWorks.RailRoad.Domain.Specification
{
    /// <summary>
    /// Define methods to search a route
    /// </summary>
    /// <typeparam name="T">Type o route</typeparam>
    public interface IRouteSearchSpecification<in T>
    {
        /// <summary>
        /// Get a value indicating if a route search e satisfied.
        /// </summary>
        /// <param name="entity">Route</param>
        /// <returns>Boolean value</returns>
        bool IsSearchSatisfiedBy(T entity);

        /// <summary>
        /// Get a value indicating if a route search can keep scanning for more results.
        /// </summary>
        /// <param name="entity">Route</param>
        /// <returns>Boolean value</returns>
        bool CanKeepSearching(T entity);
        
       
    }

}
