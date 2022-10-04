using Newtonsoft.Json;
using StockAnalyzer.Core;
using StockAnalyzer.Core.Domain;
using StockAnalyzer.Core.Services;
using StockAnalyzer.Windows.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace StockAnalyzer.Windows;

public partial class MainWindow : Window
{
    private static string API_URL = "https://ps-async.fekberg.com/api/stocks";
    private Stopwatch stopwatch = new Stopwatch();
    private Random random = new Random();

    public MainWindow()
    {
        InitializeComponent();
    }



    private async void Search_Click(object sender, RoutedEventArgs e)
    {
        BeforeLoadingStockData();

        var stocks = new Dictionary<string, IEnumerable<StockPrice>>
            {
                { "MSFT", Generate("MSFT") },
                { "GOOGL", Generate("GOOGL") },
                { "AAPL", Generate("AAPL") },
                { "CAT", Generate("CAT") },
                { "ABC", Generate("ABC") },
                { "DEF", Generate("DEF") }
            };

        var bag = new ConcurrentBag<StockCalculation>();

        try
        {
            await Task.Run(() =>
            {
                try
                {
                    Parallel.For(0, 10, (i, state) => {
                        // i == current index
                    });

                    var parallelLoopResult = Parallel.ForEach(stocks,
                        new ParallelOptions { MaxDegreeOfParallelism = 1 },
                        (element, state) => {
                            if (element.Key == "MSFT" || state.ShouldExitCurrentIteration)
                            {
                                state.Break();

                                return;
                            }
                            else
                            {
                                var result = Calculate(element.Value);
                                bag.Add(result);
                            }
                        });
                }
                catch (Exception ex)
                {
                    throw;
                }
            });
        }
        catch (Exception ex)
        {
            Notes.Text = ex.Message;
        }
        Stocks.ItemsSource = bag;

        AfterLoadingStockData();
    }











    private StockCalculation Calculate(IEnumerable<StockPrice> prices)
    {
        #region Start stopwatch
        var calculation = new StockCalculation();
        var watch = new Stopwatch();
        watch.Start();
        #endregion

        var end = DateTime.UtcNow.AddSeconds(4);

        // Spin a loop for a few seconds to simulate load
        while (DateTime.UtcNow < end)
        { }

        #region Return a result
        calculation.Identifier = prices.First().Identifier;
        calculation.Result = prices.Average(s => s.Open);

        watch.Stop();

        calculation.TotalSeconds = watch.Elapsed.Seconds;

        return calculation;
        #endregion
    }

    private IEnumerable<StockPrice> Generate(string stockIdentifier)
    {
        return Enumerable.Range(1, random.Next(10, 250))
            .Select(x => new StockPrice
            {
                Identifier = stockIdentifier,
                Open = random.Next(10, 1024)
            });
    }











    private void BeforeLoadingStockData()
    {
        stopwatch.Restart();
        StockProgress.Visibility = Visibility.Visible;
        StockProgress.IsIndeterminate = true;
    }

    private void AfterLoadingStockData()
    {
        StocksStatus.Text = $"Loaded stocks for {StockIdentifier.Text} in {stopwatch.ElapsedMilliseconds}ms";
        StockProgress.Visibility = Visibility.Hidden;
    }

    private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        Process.Start(new ProcessStartInfo { FileName = e.Uri.AbsoluteUri, UseShellExecute = true });

        e.Handled = true;
    }

    private void Close_OnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}