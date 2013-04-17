using System;
using System.Collections.Generic;
using System.Linq;
using ThoughtWorks.RailRoad.Domain.Locations;
using ThoughtWorks.RailRoad.Domain.Specification;

namespace ThoughtWorks.RailRoad.UI.Application
{
    public class SearchSpecificationGenerator
    {
        private City _destinationCity;
        private City _originCity;
        private decimal? _maxDistance;
        private decimal? _minDistance;
        private int? _maxStops;
        private int? _minStops;
        private bool? _keepScanning;

        public SearchSpecificationGenerator()
        {
            CleanSearchParameters();
        }

        public SearchSpecificationGenerator WithDestination(
            string originCityName, string destinationCityName, bool keepScanning
        )
        {
            if(string.IsNullOrEmpty(originCityName) && string.IsNullOrWhiteSpace(originCityName))
                throw new ArgumentException(@"Origin cannot be empty or null","originCityName");
            if (string.IsNullOrEmpty(destinationCityName) && string.IsNullOrWhiteSpace(destinationCityName))
                throw new ArgumentException(@"Destination cannot be empty or null", "destinationCityName"); 

            _originCity = new City(originCityName);
            _destinationCity = new City(destinationCityName);
            _keepScanning = keepScanning;

            return this;
        }

        public SearchSpecificationGenerator WithDistance(decimal minDistance, decimal maxDistance)
        {
            _minDistance = minDistance;
            _maxDistance = maxDistance;
            return this;
        }

        public SearchSpecificationGenerator WithStops(int minStops, int maxStops)
        {
            _minStops = minStops;
            _maxStops = maxStops;
            return this;
        }

        private void CleanSearchParameters()
        {
            _destinationCity = null;
            _originCity = null;
            _maxDistance = null;
            _minDistance = null;
            _maxStops = null;
            _minStops = null;
            _keepScanning = null;
        }

        public IRouteSearchSpecification<Route> Generate()
        {
            var searchSpecifications = new List<IRouteSearchSpecification<Route>>();

            if (IsDestinationConfigured())
            {
                searchSpecifications.Add(
                    new OriginDestinationSearchSpecification(_originCity, _destinationCity, _keepScanning.Value)
                );
            }
            if (IsStopsConfigured())
            {
                searchSpecifications.Add(
                    new StopSearchSpecification(_minStops.Value, _maxStops.Value)
                );
            }
            if (IsDistanceConfigured())
            {
                searchSpecifications.Add(
                    new DistanceSearchSpecification(_minDistance.Value, _maxDistance.Value)
                );
            }


            return GenerateSearchSpecificationCompilation(searchSpecifications);
        }

        private IRouteSearchSpecification<Route> GenerateSearchSpecificationCompilation(
            IEnumerable<IRouteSearchSpecification<Route>> listSearchSpecifications
        )
        {
            IRouteSearchSpecification<Route> result;
            var routeSearchSpecifications = listSearchSpecifications as IRouteSearchSpecification<Route>[] ?? listSearchSpecifications.ToArray();
            if (routeSearchSpecifications.Any())
            {
                result = routeSearchSpecifications.Count() > 1 ? 
                    new AndSearchSpecification<Route>(routeSearchSpecifications.ToArray()) 
                    : routeSearchSpecifications.First();
            }
            else
            {
                result = new StopSearchSpecification(1,50);
            }

            return result;
        }

        private bool IsDestinationConfigured()
        {
            return _destinationCity != null && _originCity != null && _keepScanning.HasValue;
        }

        private bool IsStopsConfigured()
        {
            return _minStops.HasValue && _maxStops.HasValue;
        }

        private bool IsDistanceConfigured()
        {
            return _minDistance.HasValue && _maxDistance.HasValue;
        }

        
    }
}
