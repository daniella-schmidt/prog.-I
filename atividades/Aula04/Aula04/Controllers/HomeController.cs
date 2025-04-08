using System.Diagnostics;
using System.Text;
using Aula04.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aula04.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

        [HttpGet]
        public string PrintNaturalFor(int n = 10)
        {
            string retorno = string.Empty;

            int i = 1;
            while (i <= n)
            {
                retorno += $" {i} ";
                i++;
            }

            return retorno;
        }

        [HttpGet]
        public string PrintNaturalRecursion (int count = 10)
        {
            string retorno = string.Empty;

            retorno = NaturalNumberRecursion( 1, count);

            return retorno;

        }
        public string NaturalNumberRecursion(int n, int count)
        {
            string ret = string.Empty;

            //Caso base= quando vai parar de chamar ele de novo
            // caso base: se o contador for menor que 1
            if (count <= 1)
            {
                return $" {n} ";
            }

            ret += $"{n}";
            count--; ; // decrementando


            //chamada recursiva: incrementa n e decrementa count, para imprimir o numero.
            ret += NaturalNumberRecursion( n+1 , count);

            return ret;
        }

        /*1 - Escreva um programa em c# para imprimir os números de n até 1 (decrementar)*/
        public string Ativ1(int n = 10)
        {
            string retorno = string.Empty;

            int i = n;
            while (i >= 1)
            {
                retorno += $" {i} ";
                i--; 
            }

            return retorno;
        }
        /*2 - Escreva um programa capaz de sumarizar os números de 1 a n por exemplo: n = 10 [1+2+4+5+6+7+8+9+10]*/
        public string Ativ2(int n = 10)
        {
            int soma = 0;
            string retorno = string.Empty;

            for (int i = 1; i <= n; i++) 
            {
                soma += i; 
                retorno += i == n ? $"{i}" : $"{i} + "; 
            }

            retorno += $" = {soma}"; 
            return retorno;
        }

        /*3 - Escreva um programa recursivo capaz de contar quantos caracteres tem uma string*/
        public string Ativ3()
        {
            string palavra = "O Nome da Rosa";
            int totalCaracteres = ContarCaracteresRecursivamente(palavra);
            return ($"A string possui {totalCaracteres} caracteres.");
        }
        public int ContarCaracteresRecursivamente(string palavra, int index = 0)
        {

            if (index >= palavra.Length)
            {
                return 0;
            }
            return 1 + ContarCaracteresRecursivamente(palavra, index + 1);
        }
        /*4  - Escreva um programa recursivo que seja capaz de identificar um palíndromo. */
        public bool Ativ4(string palavra)
        {
            if (string.IsNullOrEmpty(palavra))
                return true;

            palavra = new string(palavra.Where(char.IsLetter).ToArray()).ToLower();

            if (palavra.Length <= 1)
                return true;

            if (palavra[0] != palavra[^1])
                return false;

            return Ativ4(palavra[1..^1]);
        }

        public string Main(string[] args)
        {
            string[] palavrasParaTestar = { "arara" };
            StringBuilder resultados = new StringBuilder();

            foreach (var palavra in palavrasParaTestar)
            {
                bool resultado = Ativ4(palavra);
                resultados.AppendLine($"A palavra '{palavra ?? "null"}' é um palíndromo? {resultado}");
            }

            return resultados.ToString();
        }

    }

}