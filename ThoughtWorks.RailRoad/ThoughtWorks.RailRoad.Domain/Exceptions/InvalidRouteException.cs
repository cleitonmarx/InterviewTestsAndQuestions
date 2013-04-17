using System;

namespace ThoughtWorks.RailRoad.Domain.Exceptions
{
    /// <summary>
    /// The exception that is thrown when route information is invalid
    /// </summary>
    public class InvalidRouteException : Exception
    {
        public InvalidRouteException(string message) :base(message) { }
    }
}
