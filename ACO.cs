using System;
using System.Collections.Generic;
using System.Linq;

class ACO
{
    private int numberOfNodes;
    private double[,] distances;
    private double[,] pheromones;
    private double alpha;
    private double beta;
    private double evaporationRate;
    private int numberOfAnts;
    private int maxIterations;

    public ACO(int nodes, double[,] dist, int ants, int iterations, double a, double b, double evapRate)
    {
        numberOfNodes = nodes;
        distances = dist;
        pheromones = new double[nodes, nodes];
        alpha = a;
        beta = b;
        evaporationRate = evapRate;
        numberOfAnts = ants;
        maxIterations = iterations;

        // Initialize pheromones
        for (int i = 0; i < nodes; i++)
            for (int j = 0; j < nodes; j++)
                pheromones[i, j] = 1.0;
    }

    public int[] FindShortestPath()
    {
        int[] bestPath = null;
        double bestPathLength = double.MaxValue;

        for (int iteration = 0; iteration < maxIterations; iteration++)
        {
            List<int[]> allPaths = new List<int[]>();
            List<double> allPathLengths = new List<double>();

            for (int ant = 0; ant < numberOfAnts; ant++)
            {
                int[] path = ConstructPath();
                double pathLength = CalculatePathLength(path);

                allPaths.Add(path);
                allPathLengths.Add(pathLength);

                if (pathLength < bestPathLength)
                {
                    bestPath = path;
                    bestPathLength = pathLength;
                }
            }

            UpdatePheromones(allPaths, allPathLengths);
        }

        Console.WriteLine("Shortest path found: " + bestPathLength);
        return bestPath;
    }

    private int[] ConstructPath()
    {
        List<int> unvisited = Enumerable.Range(0, numberOfNodes).ToList();
        int[] path = new int[numberOfNodes];
        int currentNode = 0;

        for (int i = 0; i < numberOfNodes; i++)
        {
            path[i] = currentNode;
            unvisited.Remove(currentNode);

            if (unvisited.Count > 0)
            {
                currentNode = SelectNextNode(currentNode, unvisited);
            }
        }

        return path;
    }

    private int SelectNextNode(int currentNode, List<int> unvisited)
    {
        double[] probabilities = new double[unvisited.Count];
        double totalProbability = 0.0;

        for (int i = 0; i < unvisited.Count; i++)
        {
            int nextNode = unvisited[i];
            double pheromone = Math.Pow(pheromones[currentNode, nextNode], alpha);
            double heuristic = Math.Pow(1.0 / distances[currentNode, nextNode], beta);

            probabilities[i] = pheromone * heuristic;
            totalProbability += probabilities[i];
        }

        double random = new Random().NextDouble() * totalProbability;
        double cumulativeProbability = 0.0;

        for (int i = 0; i < probabilities.Length; i++)
        {
            cumulativeProbability += probabilities[i];
            if (random <= cumulativeProbability)
            {
                return unvisited[i];
            }
        }

        return unvisited.Last();
    }

    private double CalculatePathLength(int[] path)
    {
        double length = 0.0;
        for (int i = 0; i < path.Length - 1; i++)
        {
            length += distances[path[i], path[i + 1]];
        }
        length += distances[path.Last(), path.First()];
        return length;
    }

    private void UpdatePheromones(List<int[]> allPaths, List<double> allPathLengths)
    {
        for (int i = 0; i < numberOfNodes; i++)
            for (int j = 0; j < numberOfNodes; j++)
                pheromones[i, j] *= (1 - evaporationRate);

        for (int k = 0; k < allPaths.Count; k++)
        {
            int[] path = allPaths[k];
            double length = allPathLengths[k];

            for (int i = 0; i < path.Length - 1; i++)
            {
                pheromones[path[i], path[i + 1]] += 1.0 / length;
                pheromones[path[i + 1], path[i]] += 1.0 / length;
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Example distance matrix
        double[,] distances = {
            { 0, 2, 9, 10 },
            { 1, 0, 6, 4 },
            { 15, 7, 0, 8 },
            { 6, 3, 12, 0 }
        };

        ACO aco = new ACO(4, distances, 10, 100, 1.0, 2.0, 0.5);
        int[] bestPath = aco.FindShortestPath();

        Console.WriteLine("Optimal path:");
        foreach (int node in bestPath)
        {
            Console.Write(node + " ");
        }
    }
}
