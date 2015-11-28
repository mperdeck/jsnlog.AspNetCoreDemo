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
        private Constants _constants;

        public HomeController(ILoggerFactory loggerFactory, Constants constants)
        {
            _logger = loggerFactory.CreateLogger<HomeController>();
            _constants = constants;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Home page opened at {requestTime}", DateTime.Now);

            ViewData["LogFilePath"] = _constants.LogFilePath;

            return View();
        }
    }
}
