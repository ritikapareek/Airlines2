using AirlineNetwork.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static AirlineNetwork.Helper.AirlineHelper;

namespace AirlineNetwork.Services
{
    public class AirlineService : IAirlineService
    {
        private static readonly string projectDirectory = Directory.GetCurrentDirectory();
        private readonly string _mAirlinePath = string.Concat(projectDirectory, @"\Data\test\Airline.csv");
        private readonly string _mAirPortsPath = string.Concat(projectDirectory, @"\Data\test\Airports.csv");
        private readonly string _mRoutesPath = string.Concat(projectDirectory, @"\Data\test\Routes.csv");
        private static List<Airline> airlines;
        private static List<Airports> airports;
        private static List<Routes> routes;

        public AirlineService()
        {
            airlines = ReadCSVAirlineFile(_mAirlinePath);
            airports = ReadCSVAirportsFile(_mAirPortsPath);
            routes = ReadCSVRoutesFile(_mRoutesPath);
        }
        public async Task<List<string>> GetRoutes(string source, string destination)
        {
          
            List<string> shortestRoutes = new List<string>();
            if (airports.All(a => a.IATA3 != source))
            {
                shortestRoutes.Add("Invalid source");
                return await Task.FromResult(shortestRoutes);
            }
            if (airports.All(a => a.IATA3 != destination))
            {
                shortestRoutes.Add("Invalid destination");
                return await Task.FromResult(shortestRoutes);
            }
            List<Node> vertices = new List<Node>();

            foreach (var airport in airports)
            {
                var node = new Node(airport.IATA3);
                vertices.Add(node);
            }

            List<Tuple<Node, Node>> edges = new List<Tuple<Node, Node>>();
            foreach (var route in routes)
            {

                var nodeFrom = new Node(route.Origin);
                var nodeTo = new Node(route.Destination);
                var edge = Tuple.Create(nodeTo, nodeFrom);
                edges.Add(edge);

            }


            var graph = new Graph(vertices, edges);
            Node start = new Node(source);
            Node end = new Node(destination);

            return await GetShortestRoute(graph, start, end);
        }

        public async Task<List<string>> GetShortestRoute(Graph graph, Node source, Node destination)
        {
            //BFS to find shortest path
            Queue<Node> queue = new Queue<Node>();
            Dictionary<Node, bool> visited = new Dictionary<Node, bool>(new NodeEqualityComparator());
            Dictionary<Node, Node> prev = new Dictionary<Node, Node>(new NodeEqualityComparator());
            queue.Enqueue(source);
            visited[source] = true;
            while (queue.Count > 0)
            {
                Node curAirport = queue.Dequeue();
                if (graph.AdjacencyList.ContainsKey(curAirport))
                {
                    foreach (var neighbor in graph.AdjacencyList[curAirport])
                    {
                        if (!visited.ContainsKey(neighbor) || !visited[neighbor])
                        {
                            queue.Enqueue(neighbor);
                            visited[neighbor] = true;
                            prev[neighbor] = curAirport;
                            if (neighbor == destination)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            //construct shortest path
            List<string> shortestRoutes = new List<string>() ;

            for (var a = destination; prev.ContainsKey(a); a = prev[a])
            {
                shortestRoutes.Insert(0, prev[a].AirportCode);
            }
            if (shortestRoutes.Count == 0)
            {
                shortestRoutes.Add("No route");

            }
            else
            {
                shortestRoutes.Add(destination.AirportCode);
            }

            return await Task.FromResult(shortestRoutes);
        }
    }
}
