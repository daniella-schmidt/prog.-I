using System.Diagnostics;
using Aula05.Models;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Aula05.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private Order _order;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Customer c1 = new Customer();
            c1.Name = "Jeno";
            c1.ObjectCount++; // = 1
            Customer.InstanceCount++;// =1

            var c2 = new Customer();
            c2.Name = "Haechan";
            c2.ObjectCount++;//=1
            Customer.InstanceCount++;//=2
            //InstanceCount é da classe os objetos nada irão interferir nela.

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
