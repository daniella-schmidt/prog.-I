using Microsoft.AspNetCore.Mvc;
using Atividade1703.Models;

namespace Atividade1703.Controllers
{
    public class NumeroController : Controller
    {
            public IActionResult Index()
            {
                return View();
            }

            // Ação POST: Lida com o envio de dados do formulário
            [HttpPost]
            public IActionResult Index(int numeroInput)
            {
                // Validação
                if (numeroInput < 0 || numeroInput > 9999)
                {
                    ViewBag.NumeroPorExtenso = "Número fora do intervalo suportado.";
                    return View();
                }

                // Cria uma instância do modelo com o número inserido
                var numero = new AtividadeModel { Valor = numeroInput };

                // Usa o método do modelo para converter o número para texto por extenso
                ViewBag.NumeroPorExtenso = numero.PorExtenso();
            return View();
        }
    }
}