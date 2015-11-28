using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        private ILogger _logger;

        public HomeController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HomeController>();
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Home page opened at {requestTime}", DateTime.Now);

            return View();
        }
    }
}
