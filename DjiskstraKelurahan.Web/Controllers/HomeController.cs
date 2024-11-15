using DjiskstraKelurahan.Web.Models;
using DjiskstraKelurahan.Web.Models.Home;
using DjiskstraKelurahan.Web.Services;
using Kruskal.Core;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DjiskstraKelurahan.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IKelurahanService _kelurahanService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IKelurahanService kelurahanService)
        {
            _logger = logger;
            _kelurahanService = kelurahanService;
        }

        public IActionResult Index()
        {
            return View(new IndexVM());
        }

        [HttpPost]
        public IActionResult Index(IndexVM vm)
        {
            if(vm.Start is null || vm.End is null) 
                return View(vm);

            vm.Origin = _kelurahanService.GetByName(vm.Start)!;
            vm.Destinations = _kelurahanService.GetByName(vm.End)!;

            var start = new Vertex<Kelurahan>(vm.Origin!);
            var end = new Vertex<Kelurahan>(vm.Destinations!);
            var result = _kelurahanService.GetGraph().Djikstra(start, end);

            vm.Path = result;

            return View(vm);
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
