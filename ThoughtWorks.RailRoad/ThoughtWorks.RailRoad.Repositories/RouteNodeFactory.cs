using System;
using ThoughtWorks.RailRoad.Domain.Locations;

namespace ThoughtWorks.RailRoad.Repositories
{
    /// <summary>
    /// Factory is responsible for creation of route nodes.
    /// </summary>
    public static class RouteNodeFactory
    {
        /// <summary>
        /// Create a rote node
        /// </summary>
        /// <param name="data">Data from file</param>
        /// <returns></returns>
        public static RouteNode CreateRouteNode(string data)
        {
            if(string.IsNullOrEmpty(data)) throw new ArgumentNullException("data","Data cannot be empty or null");
            if(data.Length != 3) throw new ArgumentException("Invalid data argument","data");

            return new RouteNode(
                new City(data[0].ToString()), new City(data[1].ToString()), int.Parse(data[2].ToString())  
            );
        }
    }
}
