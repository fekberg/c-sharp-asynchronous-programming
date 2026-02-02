using System.Diagnostics;

namespace StockAnalyzer.AdvancedTopics;

internal class Program
{
    static void Main(string[] args)
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();

        var result = Enumerable.Range(0, 100)
            .AsParallel()
            .AsOrdered()
            .WithCancellation(new(canceled: true))
            .Select(Compute)
            .Take(10);

        result.ForAll(Console.WriteLine);

        Console.WriteLine($"It took: {stopwatch.ElapsedMilliseconds}ms to run");
        Console.ReadLine();
    }

    static Random random = new();
    static decimal Compute(int value)
    {
        var randomMilliseconds = random.Next(10, 50);
        var end = DateTime.Now + TimeSpan.FromMilliseconds(randomMilliseconds);

        // This will spin for a while...
        while(DateTime.Now < end) { }

        return value + 0.5m;
    }
}