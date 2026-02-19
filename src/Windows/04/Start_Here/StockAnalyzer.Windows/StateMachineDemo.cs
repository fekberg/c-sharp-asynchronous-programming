using System.Threading.Tasks;

namespace StockAnalyzer.Windows;

public class StateMachineDemo
{
    public async Task<string> Run() 
    {
        return await Compute();
    }

    public async Task<string> Compute()
    {
        return await Load();
    }

    public async Task<string> Load()
    {
        return await Task.Run(() => "Pluralsight");
    }
}
