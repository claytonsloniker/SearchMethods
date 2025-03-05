using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SearchMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            Boolean continueRouting = true;
            FileHandler fileHandler = new FileHandler();
            String? adjacenciesFile = "../../../Adjacencies.txt";
            String? coordinatesFile = "../../../coordinates.csv";
            Dictionary<string, List<string>> cityPairs = new Dictionary<string, List<string>>();
            Dictionary<string, (double Latitude, double Longitude)> cityCoordinates = new Dictionary<string, (double Latitude, double Longitude)>();

            try
            {
                cityPairs = fileHandler.ReadCityPairs(adjacenciesFile);
                cityCoordinates = fileHandler.ReadCityCoordinates(coordinatesFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the files: {ex.Message}");
                return;
            }

            while (continueRouting)
            {
                Console.WriteLine("--Town Route--");

                Console.WriteLine("Enter the starting town: ");
                String? startingTown = Console.ReadLine();

                Console.WriteLine("Enter the destination town: ");
                String? destinationTown = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(startingTown) || string.IsNullOrWhiteSpace(destinationTown))
                {
                    Console.WriteLine("Starting and destination towns cannot be empty.");
                    continue;
                }

                Console.WriteLine();
                Console.WriteLine("Enter your desired search method: ");
                Console.WriteLine("1. Breadth First Search");
                Console.WriteLine("2. Depth First Search");
                Console.WriteLine("3. ID-DFS Search");
                Console.WriteLine("4. Best First Search");
                Console.WriteLine("5. A* Search");
                Console.WriteLine("6. Exit");

                String? searchMethod = Console.ReadLine();

                Stopwatch stopwatch = new Stopwatch();

                try
                {
                    stopwatch.Start();

                    List<string> traversed = null;

                    switch (searchMethod)
                    {
                        case "1":
                            var bfs = new BreadthFirstSearch();
                            traversed = bfs.Execute(cityPairs, cityCoordinates, startingTown, destinationTown);
                            break;
                        case "2":
                            var dfs = new DepthFirstSearch();
                            traversed = dfs.Execute(cityPairs, cityCoordinates, startingTown, destinationTown);
                            break;
                        case "3":
                            var iddfs = new IDDFSearch();
                            traversed = iddfs.Execute(cityPairs, cityCoordinates, startingTown, destinationTown);
                            break;
                        case "4":
                            Console.WriteLine("Best First Search");
                            break;
                        case "5":
                            Console.WriteLine("A* Search");
                            break;
                        case "6":
                            Console.WriteLine("Exiting...");
                            continueRouting = false;
                            break;
                        default:
                            Console.WriteLine("Invalid search method");
                            break;
                    }

                    stopwatch.Stop();

                    if (traversed != null)
                    {
                        Console.WriteLine();
                        foreach (String? city in traversed)
                        {
                            Console.WriteLine(city);
                        }

                        Console.WriteLine();

                        // Calculate the total distance of the traversal
                        Distance distanceCalculator = new Distance();
                        double totalDistance = distanceCalculator.CalculateTotalDistance(traversed, cityCoordinates);
                        Console.WriteLine($"Total Distance: {totalDistance:0.00} km");
                        Console.WriteLine($"Total Time: {stopwatch.ElapsedMilliseconds} ms");
                    }
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();
                    Console.WriteLine($"An error occurred during the search: {ex.Message}");
                }

                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
