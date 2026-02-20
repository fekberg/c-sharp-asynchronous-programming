
using System.Diagnostics;

Stopwatch stopwatch = new();
stopwatch.Start();

Lock lockObject = new();
decimal total = 0;

Parallel.For(0, 100, (i) => {
    var result = Compute(i);
    lock (lockObject) // Only lock for a short time!
    {
        total += result;
    }
});

Console.WriteLine(total);
Console.WriteLine($"It took: {stopwatch.ElapsedMilliseconds}ms to run");
Console.ReadLine();

decimal Compute(int value)
{
    var randomMilliseconds = Random.Shared.Next(10, 50);
    var end = DateTime.Now + TimeSpan.FromMilliseconds(randomMilliseconds);

    // This will spin for a while...
    while (DateTime.Now < end) { }

    return value + 0.5m;
}