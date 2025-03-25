using System.Diagnostics;
using Aula2403.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aula2403.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult ObterDia(string indice)
        {
            //criar um vetor com os dias da semana e de acordo com o usuario digite apareca o dia da semana de acorod com o vetor
            string[] diasSemana = { "Domingo", "Segunda-feira", "Terça-feira", "Quarta-feira", "Quinta-feira", "Sexta-feira", "Sábado" };

            if (int.TryParse(indice, out int i) && i >= 0 && i < diasSemana.Length)
            {
                ViewBag.DiaEscolhido = diasSemana[i];
            }
            else
            {
                ViewBag.DiaEscolhido = "Entrada inválida. Digite um número entre 0 e 6.";
            }

            return View("Index");
        }
        public IActionResult Index()
        {
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
