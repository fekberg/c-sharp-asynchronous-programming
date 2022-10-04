using System.Diagnostics;

namespace StockAnalyzer.AdvancedTopics;

internal class Program
{
    static object syncRoot = new();
    static object lock1 = new();
    static object lock2 = new();

    static async Task Main(string[] args)
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();

        var t1 = Task.Run(() => { 
            lock(lock1)
            {
                Thread.Sleep(1);
                lock(lock2)
                {
                    Console.WriteLine("Hello!");
                }
            }
        });
        var t2 = Task.Run(() => { 
            lock(lock2)
            {
                Thread.Sleep(1);
                lock(lock1)
                {
                    Console.WriteLine("Hello..?");
                }
            }
        });

        await Task.WhenAll(t1, t2);

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