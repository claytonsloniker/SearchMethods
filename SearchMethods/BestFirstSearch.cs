using System;
using System.Collections.Generic;

namespace SearchMethods
{
    public class BestFirstSearch
    {
        public List<string> Execute(Dictionary<string, List<string>> cityPairs, Dictionary<string, (double Latitude, double Longitude)> cityCoordinates, string startingTown, string destinationTown)
        {
            if (!cityPairs.ContainsKey(startingTown) || !cityPairs.ContainsKey(destinationTown))
            {
                Console.WriteLine("Invalid starting or destination town.");
                return new List<string>();
            }

            Dictionary<string, double> distances = new Dictionary<string, double>();
            Distance distanceCalc = new Distance();
            string currentCity = startingTown;

            // Initialize all distances to infinity
            foreach (var city in cityPairs.Keys)
            {
                distances[city] = double.MaxValue;
            }
            distances[startingTown] = 0;

            HashSet<string> visited = new HashSet<string>();
            List<string> traversal = new List<string>();

            // Priority queue (min-heap) to store cities based on their heuristic values
            PriorityQueue<string, double> pq = new PriorityQueue<string, double>();
            pq.Enqueue(startingTown, 0);

            while (pq.Count > 0)
            {
                currentCity = pq.Dequeue();

                if (currentCity == destinationTown)
                {
                    traversal.Add(currentCity);
                    break;
                }

                if (!visited.Contains(currentCity))
                {
                    visited.Add(currentCity);
                    traversal.Add(currentCity);

                    foreach (var neighbor in cityPairs[currentCity])
                    {
                        if (!visited.Contains(neighbor))
                        {
                            double distance = distanceCalc.CalculateDistance(cityCoordinates[currentCity], cityCoordinates[neighbor]);
                            if (distances[currentCity] + distance < distances[neighbor])
                            {
                                distances[neighbor] = distances[currentCity] + distance;
                                pq.Enqueue(neighbor, distances[neighbor]);
                            }
                        }
                    }
                }
            }

            return traversal;
        }
    }
}
