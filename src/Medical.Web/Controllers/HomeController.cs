using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Medical.Web.Models;
using Medical.Entities.System;
using System.Net.Http;

namespace Medical.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListModules()
        {
            IEnumerable<vnc_Modules> modules = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8088/api/");
                var responseTask = client.GetAsync("modules");
                responseTask.Wait();
                //To store result of web api response.   
                var result = responseTask.Result;
                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<vnc_Modules>>();
                    readTask.Wait();
                    modules = readTask.Result;
                }
                else
                {                  
                    modules = Enumerable.Empty<vnc_Modules>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(modules);
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
