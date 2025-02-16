using DIExampleWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Stone.ServiceContract;

namespace DIExampleWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICitiesService _citiesService1;
        private readonly ICitiesService _citiesService2;
        private readonly ICitiesService _citiesService3;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public HomeController(ILogger<HomeController> logger
            ,ICitiesService citiesService1
            ,ICitiesService citiesService2
            ,ICitiesService citiesService3
            ,IServiceScopeFactory serviceScopeFactory
            )
        {
            _logger = logger;
            _citiesService1 = citiesService1;
            _citiesService2 = citiesService2;
            _citiesService3 = citiesService3;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public IActionResult Index()
        {
            var cities = _citiesService1.GetCities();
            ViewBag._citiesService1_InstanceID = _citiesService1.ServiceInstanceID;
            ViewBag._citiesService2_InstanceID = _citiesService2.ServiceInstanceID;
            ViewBag._citiesService3_InstanceID = _citiesService3.ServiceInstanceID;

            using(IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                // Inject Services
                ICitiesService citiesService = scope.ServiceProvider.GetRequiredService<ICitiesService>();
                ViewBag._citiesService4_InstanceID = citiesService.ServiceInstanceID;

            }// Dispose


            return View(cities);
        }

        public IActionResult Index2([FromServices]ICitiesService citiesService)
        {
           var cities = citiesService.GetCities();


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
