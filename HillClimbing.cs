using System;

class HillClimbing
{
    public static int[] OptimizeResourceAllocation(int points, int resources, int[] priorities)
    {
        var random = new Random();
        int[] allocation = new int[points];
        for (int i = 0; i < points; i++)
            allocation[i] = random.Next(1, resources / points + 1);

        double bestScore = EvaluateFitness(allocation, priorities);
        bool improvement = true;

        while (improvement)
        {
            improvement = false;

            for (int i = 0; i < points; i++)
            {
                int oldValue = allocation[i];
                allocation[i] = Math.Min(resources, allocation[i] + 1);
                double newScore = EvaluateFitness(allocation, priorities);

                if (newScore > bestScore)
                {
                    bestScore = newScore;
                    improvement = true;
                }
                else
                {
                    allocation[i] = oldValue;
                }
            }
        }

        return allocation;
    }

    private static double EvaluateFitness(int[] allocation, int[] priorities)
    {
        double score = 0;
        for (int i = 0; i < allocation.Length; i++)
        {
            score += allocation[i] * priorities[i];
        }
        return score;
    }
}

class ResourceProgram
{
   /*static void Main(string[] args)
    {
        int[] priorities = { 3, 5, 2, 4 }; // Priorities for each point
        int totalResources = 10;

        int[] allocation = HillClimbing.OptimizeResourceAllocation(4, totalResources, priorities);
        Console.WriteLine("Optimized resource allocation:");
        for (int i = 0; i < allocation.Length; i++)
        {
            Console.WriteLine($"Point {i + 1}: {allocation[i]} resources");
        }
    }*/
}
