using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockAnalyzer.Core.Services;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockAnalyzer.Tests
{
    [TestClass]
    public class MockStockServiceTests
    {
        [TestMethod]
        public async Task Can_Load_All_MSFT_Stocks()
        {
            var service = new MockStockService();
            var stocks = await service.GetStockPricesFor("MSFT",
                CancellationToken.None);

            Assert.AreEqual(stocks.Count(), 2);
        }
    }
}
