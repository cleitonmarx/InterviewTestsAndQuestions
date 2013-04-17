using System;

namespace ThoughtWorks.RailRoad.Domain.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a route not exists
    /// </summary>
    public class NoSuchRouteException : Exception
    {
        public NoSuchRouteException() : base("NO SUCH ROUTE"){ }
    }
}
