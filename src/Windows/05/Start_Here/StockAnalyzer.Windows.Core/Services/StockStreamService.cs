using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using StockAnalyzer.Core.Domain;

namespace StockAnalyzer.Windows.Services
{
    public interface IStockStreamService
    {
        IAsyncEnumerable<StockPrice>
            GetAllStockPrices(CancellationToken cancellationToken = default);
    }
}
