using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockAnalyzer.AttachedDetatched
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting");

            var task = Task.Factory.StartNew(async () => {

                await Task.Delay(2000);

                return "Pluralsight!";

            }).Unwrap();

            var result = await task;

            Console.WriteLine("Completed");
            Console.ReadLine();
        }
    }
}
