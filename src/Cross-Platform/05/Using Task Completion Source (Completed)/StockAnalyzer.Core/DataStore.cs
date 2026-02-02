using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using StockAnalyzer.Core.Domain;

namespace StockAnalyzer.Core;

public class DataStore
{
    private string basePath { get; }

    public DataStore(string basePath = "")
    {
        this.basePath = basePath;
    }

    public async Task<IList<StockPrice>> GetStockPrices(string stockIdentifier)
    {
        var prices = new List<StockPrice>();

        using var stream =
            new StreamReader(File.OpenRead(Path.Combine(basePath, @"StockPrices_Small.csv")));

        await stream.ReadLineAsync(); // Skip the header how in the CSV

        while (await stream.ReadLineAsync() is string line)
        {
            #region Find & Parse Stock Price from CSV
            var segments = line.Split(',');

            for (var i = 0; i < segments.Length; i++) segments[i] = segments[i].Trim('\'', '"');

            if (segments[0].ToUpperInvariant()
                != stockIdentifier.ToUpperInvariant())
            {
                continue;
            }
            #endregion

            var price = new StockPrice
            {
                Identifier = segments[0],
                TradeDate = DateTime.ParseExact(segments[1], "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                Volume = Convert.ToInt32(segments[6], CultureInfo.InvariantCulture),
                Change = Convert.ToDecimal(segments[7], CultureInfo.InvariantCulture),
                ChangePercent = Convert.ToDecimal(segments[8], CultureInfo.InvariantCulture),
            };

            prices.Add(price);
        }

        if (!prices.Any())
        {
            throw new KeyNotFoundException($"Could not find any stocks for {stockIdentifier}");
        }

        await Task.Delay(5000);

        return prices;
    }
}