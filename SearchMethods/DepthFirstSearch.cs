// REFERENCE: https://www.geeksforgeeks.org/depth-first-search-or-dfs-for-a-graph/

namespace SearchMethods
{
    public class DepthFirstSearch()
    {
        public List<string> Execute(Dictionary<string, List<string>> cityPairs, Dictionary<string, (double Latitude, double Longitude)> cityCoordinates, string startingTown, string destinationTown)
        {
            if (!cityPairs.ContainsKey(startingTown) || !cityPairs.ContainsKey(destinationTown))
            {
                Console.WriteLine("Invalid starting or destination town.");
                return new List<string>();
            }

            List<string> traversal = new List<string>();
            Stack<string> stack = new Stack<string>();
            // Mark all the vertices as not visited
            bool[] visited = new bool[cityPairs.Count];

            foreach (var city in cityPairs.Keys)
            {
                visited[cityPairs.Keys.ToList().IndexOf(city)] = false;
            }

            // Push the starting town and mark it as visited
            stack.Push(startingTown);
            visited[cityPairs.Keys.ToList().IndexOf(startingTown)] = true;

            while (stack.Count != 0)
            {
                // Pop a town from the stack
                string currentTown = stack.Pop();
                traversal.Add(currentTown);
                // If the current town is the destination town, break the loop
                if (currentTown == destinationTown)
                {
                    break;
                }
                // Push all adjacent towns of the current town
                foreach (var adjacentTown in cityPairs[currentTown])
                {
                    if (!visited[cityPairs.Keys.ToList().IndexOf(adjacentTown)])
                    {
                        stack.Push(adjacentTown);
                        visited[cityPairs.Keys.ToList().IndexOf(adjacentTown)] = true;
                    }
                }
            }
            return traversal;
        }
    }
}