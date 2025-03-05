using System;
using System.Collections.Generic;

namespace SearchMethods
{
    public class IDDFSearch
    {
        public List<string> Execute(Dictionary<string, List<string>> cityPairs, Dictionary<string, (double Latitude, double Longitude)> cityCoordinates, string startingTown, string destinationTown)
        {
            if (!cityPairs.ContainsKey(startingTown) || !cityPairs.ContainsKey(destinationTown))
            {
                Console.WriteLine("Invalid starting or destination town.");
                return new List<string>();
            }

            int maxDepth = cityPairs.Count;
            for (int depth = 0; depth <= maxDepth; depth++)
            {
                List<string> traversal = new List<string>();
                bool[] visited = new bool[cityPairs.Count];
                if (DepthLimitedSearch(cityPairs, startingTown, destinationTown, depth, traversal, visited))
                {
                    return traversal;
                }
            }
            // not found
            return new List<string>();
        }

        private bool DepthLimitedSearch(Dictionary<string, List<string>> cityPairs, string currentTown, string destinationTown, int depth, List<string> traversal, bool[] visited)
        {
            if (currentTown == destinationTown)
            {
                traversal.Add(currentTown);
                return true;
            }

            if (depth <= 0)
            {
                return false;
            }

            visited[cityPairs.Keys.ToList().IndexOf(currentTown)] = true;
            traversal.Add(currentTown);

            foreach (var adjacentTown in cityPairs[currentTown])
            {
                if (!visited[cityPairs.Keys.ToList().IndexOf(adjacentTown)])
                {
                    if (DepthLimitedSearch(cityPairs, adjacentTown, destinationTown, depth - 1, traversal, visited))
                    {
                        return true;
                    }
                }
            }

            traversal.Remove(currentTown); // Backtrack
            visited[cityPairs.Keys.ToList().IndexOf(currentTown)] = false;
            return false;
        }
    }
}
