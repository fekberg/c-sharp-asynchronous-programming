using StockAnalyzer.Core.Domain;
using System.Collections.Generic;
using System.Threading;

namespace StockAnalyzer.Windows.Services;

public interface IStockStreamService
{
    IAsyncEnumerable<StockPrice>
        GetAllStockPrices(CancellationToken cancellationToken = default);
}
