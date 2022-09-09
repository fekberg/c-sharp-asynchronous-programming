using StockAnalyzer.Core.Services;

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

            Assert.AreEqual(2, stocks.Count());
        }
    }
}