using System.Threading.Tasks;

namespace StockAnalyzer.Windows
{
    public class StateMachineDemo
    {
        public Task<string> Run()
        {
            return Compute();
        }

        public Task<string> Compute()
        {
            return Load();
        }

        public Task<string> Load()  
        {
            return Task.Run(() => "Pluralsight");
        }
    }
}
