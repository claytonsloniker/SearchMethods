// REFERENCE: https://www.geeksforgeeks.org/breadth-first-search-or-bfs-for-a-graph/

namespace SearchMethods
{
    public class BreadthFirstSearch
    {
        public List<string> Execute(Dictionary<string, List<string>> cityPairs, Dictionary<string, (double Latitude, double Longitude)> cityCoordinates, string startingTown, string destinationTown)
        {
            if (!cityPairs.ContainsKey(startingTown) || !cityPairs.ContainsKey(destinationTown))
            {
                Console.WriteLine("Invalid starting or destination town.");
                return new List<string>();
            }

            List<string> traversal = new List<string>();
            Queue<string> queue = new Queue<string>();

            // Mark all the vertices as not visited
            bool[] visited = new bool[cityPairs.Count];

            foreach (var city in cityPairs.Keys)
            {
                visited[cityPairs.Keys.ToList().IndexOf(city)] = false;
            }

            // Enqueue the starting town and mark it as visited
            queue.Enqueue(startingTown);
            visited[cityPairs.Keys.ToList().IndexOf(startingTown)] = true;

            while (queue.Count != 0)
            {
                // Dequeue a town from the queue
                string currentTown = queue.Dequeue();
                traversal.Add(currentTown);

                // If the current town is the destination town, break the loop
                if (currentTown == destinationTown)
                {
                    break;
                }

                // Enqueue all adjacent towns of the current town
                foreach (var adjacentTown in cityPairs[currentTown])
                {
                    if (!visited[cityPairs.Keys.ToList().IndexOf(adjacentTown)])
                    {
                        queue.Enqueue(adjacentTown);
                        visited[cityPairs.Keys.ToList().IndexOf(adjacentTown)] = true;
                    }
                }
            }
            return traversal;


        }
    }
}
