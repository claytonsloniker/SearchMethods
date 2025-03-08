namespace SearchMethods
{
    public class AStarSearch
    {
        public List<string> Execute(Dictionary<string, List<string>> cityPairs, Dictionary<string, (double Latitude, double Longitude)> cityCoordinates, string startingTown, string destinationTown)
        {
            if (!cityPairs.ContainsKey(startingTown) || !cityPairs.ContainsKey(destinationTown))
            {
                Console.WriteLine("Invalid starting or destination town.");
                return new List<string>();
            }

            Dictionary<string, double> gScores = new Dictionary<string, double>();
            Dictionary<string, double> fScores = new Dictionary<string, double>();
            Dictionary<string, string> cameFrom = new Dictionary<string, string>();
            Distance distanceCalc = new Distance();
            string currentCity = startingTown;

            // Initialize all gScores and fScores to infinity
            foreach (var city in cityPairs.Keys)
            {
                gScores[city] = double.MaxValue;
                fScores[city] = double.MaxValue;
            }
            gScores[startingTown] = 0;
            fScores[startingTown] = distanceCalc.CalculateDistance(cityCoordinates[startingTown], cityCoordinates[destinationTown]);

            HashSet<string> visited = new HashSet<string>();
            List<string> traversal = new List<string>();

            // Priority queue (min-heap) to store cities based on their fScores
            SortedSet<Tuple<double, string>> pq = new SortedSet<Tuple<double, string>>(new FScoreComparer());
            pq.Add(new Tuple<double, string>(fScores[startingTown], startingTown));

            while (pq.Count > 0)
            {
                var currMin = pq.Min;
                currentCity = currMin.Item2;
                pq.Remove(currMin);

                if (currentCity == destinationTown)
                {
                    return ReconstructPath(cameFrom, currentCity);
                }

                if (!visited.Contains(currentCity))
                {
                    visited.Add(currentCity);

                    foreach (var neighbor in cityPairs[currentCity])
                    {
                        if (!visited.Contains(neighbor))
                        {
                            double tentativeGScore = gScores[currentCity] + distanceCalc.CalculateDistance(cityCoordinates[currentCity], cityCoordinates[neighbor]);
                            if (tentativeGScore < gScores[neighbor])
                            {
                                cameFrom[neighbor] = currentCity;
                                gScores[neighbor] = tentativeGScore;
                                fScores[neighbor] = gScores[neighbor] + distanceCalc.CalculateDistance(cityCoordinates[neighbor], cityCoordinates[destinationTown]);
                                pq.Add(new Tuple<double, string>(fScores[neighbor], neighbor));
                            }
                        }
                    }
                }
            }

            return new List<string>(); // Return an empty list if no path is found
        }

        private List<string> ReconstructPath(Dictionary<string, string> cameFrom, string currentCity)
        {
            List<string> totalPath = new List<string> { currentCity };
            while (cameFrom.ContainsKey(currentCity))
            {
                currentCity = cameFrom[currentCity];
                totalPath.Insert(0, currentCity);
            }
            return totalPath;
        }

        // Custom comparer for the priority queue
        private class FScoreComparer : IComparer<Tuple<double, string>>
        {
            public int Compare(Tuple<double, string> x, Tuple<double, string> y)
            {
                int result = x.Item1.CompareTo(y.Item1);
                if (result == 0)
                {
                    result = x.Item2.CompareTo(y.Item2);
                }
                return result;
            }
        }
    }
}
