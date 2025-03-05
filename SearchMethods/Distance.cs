using System;
using System.Collections.Generic;

namespace SearchMethods
{
    public class Distance
    {
        private const double EarthRadiusKm = 6371.0;

        public double CalculateDistance((double Latitude, double Longitude) city1, (double Latitude, double Longitude) city2)
        {
            double lat1 = city1.Latitude;
            double lon1 = city1.Longitude;
            double lat2 = city2.Latitude;
            double lon2 = city2.Longitude;

            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EarthRadiusKm * c;
        }

        public double CalculateTotalDistance(List<string> traversal, Dictionary<string, (double Latitude, double Longitude)> cityCoordinates)
        {
            double totalDistance = 0.0;

            for (int i = 0; i < traversal.Count - 1; i++)
            {
                var city1 = cityCoordinates[traversal[i]];
                var city2 = cityCoordinates[traversal[i + 1]];
                totalDistance += CalculateDistance(city1, city2);
            }

            return totalDistance;
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
    }
}
