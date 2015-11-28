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
        private ILoggerFactory _loggerFactory;

        public HomeController(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public IActionResult Index()
        {
            //############         _logger.LogInformation("Home page opened at {requestTime}", DateTime.Now);

            Microsoft.Extensions.Logging.ILogger _logger = _loggerFactory.CreateLogger<HomeController>();

            _logger.LogInformation("home page opened at {requestTime}", DateTime.Now);



            return View();
        }
    }
}
