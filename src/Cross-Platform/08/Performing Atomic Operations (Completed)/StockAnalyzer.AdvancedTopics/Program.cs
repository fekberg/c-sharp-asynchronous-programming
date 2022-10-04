using System.Diagnostics;

namespace StockAnalyzer.AdvancedTopics;

internal class Program
{
    static object syncRoot = new();
    static void Main(string[] args)
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();

        int total = 0;

        Parallel.For(0, 100, (i) => {
            var result = Compute(i);
            Interlocked.Add(ref total, (int)result);
        });

        Console.WriteLine(total);
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