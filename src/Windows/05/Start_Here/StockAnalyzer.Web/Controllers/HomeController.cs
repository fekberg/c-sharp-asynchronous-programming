using Newtonsoft.Json;
using StockAnalyzer.Core.Domain;
using StockAnalyzer.Core.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StockAnalyzer.Web.Controllers
{
    public class HomeController : Controller
    {
        private static string API_URL = "https://ps-async.fekberg.com/api/stocks";

        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync($"{API_URL}/MSFT");

                var response = await responseTask;

                var content = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<IEnumerable<StockPrice>>(content);

                return View(data);
            }
        }



        public async Task<ActionResult> ConfigureAwaitDemo()
        {
            var context = System.Web.HttpContext.Current;

            var data = await GetStocks();

            return Json(data);
        }

        public async Task<IEnumerable<StockPrice>> GetStocks()
        {
            var service = new StockService();

            var task = service.GetStockPricesFor("MSFT", CancellationToken.None);
            
            var data = await task.ConfigureAwait(false);

            var context = System.Web.HttpContext.Current;

            return data;
        }






        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}