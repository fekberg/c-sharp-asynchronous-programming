using System.Threading.Tasks;

namespace StockAnalyzer.Windows;

public class StateMachineDemo
{
    public async Task<string> Run() 
    {
        var resultTask = Compute();

        return await resultTask;
    }

    public async Task<string> Compute()
    {
        var result = await Load();

        return result;
    }

    public async Task<string> Load()
    {
        return await Task.Run(() => "Pluralsight");
    }
}
