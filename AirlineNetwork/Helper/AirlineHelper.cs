using AirlineNetwork.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AirlineNetwork.Helper
{
    public static class AirlineHelper
    {
        public static List<Airline> ReadCSVAirlineFile(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return null;
            var lines = File.ReadAllLines(path).Skip(1).ToArray();
            var airlineData = new List<Airline>();
            try
            {
                foreach (var line in lines)
                {
                    var trimmed = line.Trim();
                    if (string.IsNullOrEmpty(trimmed)) continue;
                    var split = trimmed.Split(',');
                    if (split.Length != 4) { throw new InvalidOperationException("Each line should have exactly 4 csv-s: name, 2 digit code, 3 digit code, country"); }
                    airlineData.Add(new Airline(split[0], split[1], split[2], split[3]));
                }
                return airlineData;
            }
            catch (Exception ex)
            {
                var msg2 = $"\r\n-Unable to Create List of airlines From Csv '{path}'";
                throw new InvalidOperationException(ex.Message + msg2);
            }
        }

        public static List<Airports> ReadCSVAirportsFile(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return null;
            var lines = File.ReadAllLines(path).Skip(1).ToArray();
            var airlineData = new List<Airports>();
            try
            {
                foreach (var line in lines)
                {
                    var trimmed = line.Trim();
                    if (string.IsNullOrEmpty(trimmed)) continue;
                    var split = trimmed.Split(',');
                    if (split.Length != 6) { throw new InvalidOperationException("Each line should have exactly 4 csv-s: name,city, country, iata 3, latitude, longitude"); }
                    airlineData.Add(new Airports(split[0], split[1], split[2], split[3], split[4], split[5]));
                }
                return airlineData;
            }
            catch (Exception ex)
            {
                var msg2 = $"\r\n-Unable to Create List of airports From Csv '{path}'";
                throw new InvalidOperationException(ex.Message + msg2);
            }
        }

        public static List<Routes> ReadCSVRoutesFile(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return null;
            var lines = File.ReadAllLines(path).Skip(1).ToArray();
            var airlineData = new List<Routes>();
            try
            {
                foreach (var line in lines)
                {
                    var trimmed = line.Trim();
                    if (string.IsNullOrEmpty(trimmed)) continue;
                    var split = trimmed.Split(',');
                    if (split.Length != 3) { throw new InvalidOperationException("Each line should have exactly 4 csv-s: airline id, origin, destination"); }
                    airlineData.Add(new Routes(split[0], split[1], split[2]));
                }
                return airlineData;
            }
            catch (Exception ex)
            {
                var msg2 = $"\r\n-Unable to Create List of airlines From Csv '{path}'";
                throw new InvalidOperationException(ex.Message + msg2);
            }
        }

        public class Graph
        {
            public Graph() { }
            public Graph(IEnumerable<Node> vertices, IEnumerable<Tuple<Node, Node>> edges)
            {
                foreach (var vertex in vertices)
                    AddVertex(vertex);

                foreach (var edge in edges)
                    AddEdge(edge);
            }

            public Dictionary<Node, HashSet<Node>> AdjacencyList { get; }
                = new Dictionary<Node, HashSet<Node>>(new NodeEqualityComparator());

            public void AddVertex(Node vertex)
            {
                AdjacencyList[vertex] = new HashSet<Node>();
            }

            public void AddEdge(Tuple<Node, Node> edge)
            {
                if (AdjacencyList.ContainsKey(edge.Item1) && AdjacencyList.ContainsKey(edge.Item2))
                {
                    AdjacencyList[edge.Item1].Add(edge.Item2);
                }
            }
        }

        public class Edge
        {
            public Node From { get; private set; }

            public Node To { get; private set; }

            public Edge(Node from, Node to)
            {
                this.From = from;
                this.To = to;
            }


            public override string ToString()
            {
                return string.Format("{0} -> {1}", this.From, this.To);
            }
        }

        public class Node
        {
            public string AirportCode { get; private set; }

            public Node(string AirportCode)
            {
                this.AirportCode = AirportCode;
            }
        }

        public class NodeEqualityComparator : IEqualityComparer<Node>
        {
            public bool Equals(Node x, Node y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.ToString() == y.ToString();
            }

            public int GetHashCode(Node obj)
            {
                return obj.AirportCode.GetHashCode();
            }
        }

    }

}
