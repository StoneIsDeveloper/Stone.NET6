using DIExampleWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Stone.ServiceContract;

namespace DIExampleWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICitiesService _citiesService;
        public HomeController(ILogger<HomeController> logger,
            ICitiesService citiesService)
        {
            _logger = logger;
            _citiesService = citiesService;
        }

        public IActionResult Index()
        {
           var cities =  _citiesService.GetCities();


            return View(cities);
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
