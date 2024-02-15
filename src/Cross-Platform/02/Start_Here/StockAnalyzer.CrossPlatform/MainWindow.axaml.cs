using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Newtonsoft.Json;
using StockAnalyzer.Core.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace StockAnalyzer.CrossPlatform;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        IEX.PointerPressed += (e, a) => Open("https://iextrading.com/developer/");
        IEX_Terms.PointerPressed += (e, a) => Open("https://iextrading.com/api-exhibit-a/");

        /// Data provided for free by <a href="https://iextrading.com/developer/" RequestNavigate="Hyperlink_OnRequestNavigate">IEX</Hyperlink>. View <Hyperlink NavigateUri="https://iextrading.com/api-exhibit-a/" RequestNavigate="Hyperlink_OnRequestNavigate">IEX’s Terms of Use.</Hyperlink>
    }

    private static string API_URL = "https://ps-async.fekberg.com/api/stocks";
    private Stopwatch stopwatch = new Stopwatch();

    private void Search_Click(object sender, RoutedEventArgs e)
    {
        BeforeLoadingStockData();

        var client = new WebClient();

        var content = client.DownloadString($"{API_URL}/{StockIdentifier.Text}");

        // Simulate that the web call takes a very long time
        Thread.Sleep(10000);

        var data = JsonConvert.DeserializeObject<IEnumerable<StockPrice>>(content);

        // This is the same as ItemsSource in WPF used in the course videos
        Stocks.ItemsSource = data;

        AfterLoadingStockData();
    }








    private void BeforeLoadingStockData()
    {
        stopwatch.Restart();
        StockProgress.IsVisible = true;
        StockProgress.IsIndeterminate = true;
    }

    private void AfterLoadingStockData()
    {
        StocksStatus.Text = $"Loaded stocks for {StockIdentifier.Text} in {stopwatch.ElapsedMilliseconds}ms";
        StockProgress.IsVisible = false;
    }

    private void Close_OnClick(object sender, RoutedEventArgs e)
    {
        if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
        {
            desktopLifetime.Shutdown();
        }
    }

    public static void Open(string url)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            url = url.Replace("&", "^&");
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            Process.Start("xdg-open", url);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Process.Start("open", url);
        }
    }
}
