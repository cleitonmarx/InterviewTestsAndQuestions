namespace ThoughtWorks.RailRoad.Domain.Locations
{
    /// <summary>
    /// Represents a city with a rail station.
    /// </summary>
    public class City
    {
        public City(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
    }
}
