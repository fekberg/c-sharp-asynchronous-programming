using System.Collections.Generic;
using System.Threading;
using StockAnalyzer.Core.Domain;

namespace StockAnalyzer.Windows.Services;

public interface IStockStreamService
{
    IAsyncEnumerable<StockPrice>
        GetAllStockPrices(CancellationToken cancellationToken = default);
}