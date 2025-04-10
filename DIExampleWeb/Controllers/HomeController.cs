using DIExampleWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Stone.ServiceContract;
using Autofac;
using Microsoft.Extensions.DependencyInjection;

namespace DIExampleWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly ICitiesService _citiesService1;
        //private readonly ICitiesService _citiesService2;
        //private readonly ICitiesService _citiesService3;
        //private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILifetimeScope _lifetimeScope;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(ILogger<HomeController> logger            
            , ILifetimeScope lifetimeScope      
            , IWebHostEnvironment webHostEnvironment
            )
        {
            _logger = logger;         
            _lifetimeScope = lifetimeScope;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            //var cities = _citiesService1.GetCities();
            //  ViewBag._citiesService1_InstanceID = _citiesService1.ServiceInstanceID;
            // ViewBag._citiesService2_InstanceID = _citiesService2.ServiceInstanceID;
            // ViewBag._citiesService3_InstanceID = _citiesService3.ServiceInstanceID;
            var cities = new List<string>();
            //using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            //{
            //    // Inject Services
            //    ICitiesService citiesService = scope.ServiceProvider.GetRequiredService<ICitiesService>();
            //    ViewBag._citiesService4_InstanceID = citiesService.ServiceInstanceID;
            //    cities = citiesService.GetCities();
            //}
             
            using (ILifetimeScope scope = _lifetimeScope.BeginLifetimeScope())
            {
                // Inject Services
                ICitiesService citiesService = scope.Resolve<ICitiesService>();
                ViewBag._citiesService4_InstanceID = citiesService.ServiceInstanceID;
                cities = citiesService.GetCities();
            }// Dispose

            var isdev = _webHostEnvironment.IsDevelopment();
            var root = _webHostEnvironment.ContentRootPath;

            return View(cities);
        }

        public IActionResult Other()
        {
            return View();
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
