using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Aula01.Controllers
{
    public class Result
    {
        public string Texto { get; set; } = string.Empty;
    }

    public class AtividadeController : Controller
    {
        private const int Chave = 3;

        [HttpGet]
        public IActionResult Index()
        {
            return View("Index", new Result());
        }

        [HttpPost]
        public IActionResult Index(string texto, string operacao)
        {
            Result resultado = new Result();
            int chave = operacao == "decifrar" ? -Chave : Chave;
            resultado.Texto = AplicarCifra(texto, chave);
            return View("Index", resultado);
        }

        private static string AplicarCifra(string texto, int chave)
        {
            chave = chave % 26;
            if (chave < 0) chave += 26;

            StringBuilder resultado = new StringBuilder();
            foreach (char c in texto)
            {
                if (!char.IsLetter(c))
                {
                    resultado.Append(c);
                    continue;
                }

                char offset = char.IsUpper(c) ? 'A' : 'a';
                resultado.Append((char)((c + chave - offset) % 26 + offset));
            }
            return resultado.ToString();
        }
    }
}