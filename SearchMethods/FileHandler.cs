using System;
using System.IO;
using System.Collections.Generic;

namespace SearchMethods
{
    public class FileHandler
    {
        public Dictionary<string, List<string>> ReadCityPairs(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path cannot be empty.", nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.", filePath);
            }

            var cityPairs = new Dictionary<string, List<string>>();
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var cities = line.Split(' ');

                var city1 = cities[0].Trim();
                var city2 = cities[1].Trim();

                if (!cityPairs.ContainsKey(city1))
                {
                    cityPairs[city1] = new List<string>();
                }
                if (!cityPairs.ContainsKey(city2))
                {
                    cityPairs[city2] = new List<string>();
                }

                cityPairs[city1].Add(city2);
                cityPairs[city2].Add(city1);
            }

            return cityPairs;
        }

        public Dictionary<string, (double Latitude, double Longitude)> ReadCityCoordinates(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path cannot be empty.", nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.", filePath);
            }

            var cityCoordinates = new Dictionary<string, (double Latitude, double Longitude)>();
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var parts = line.Split(',');

                var cityName = parts[0].Trim();
                if (!double.TryParse(parts[1].Trim(), out double latitude))
                {
                    throw new FormatException($"Invalid latitude value for city {cityName}.");
                }
                if (!double.TryParse(parts[2].Trim(), out double longitude))
                {
                    throw new FormatException($"Invalid longitude value for city {cityName}.");
                }

                cityCoordinates[cityName] = (latitude, longitude);
            }

            return cityCoordinates;
        }
    }
}
