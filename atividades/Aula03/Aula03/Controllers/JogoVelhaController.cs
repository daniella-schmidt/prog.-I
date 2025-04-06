using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Aula03;

namespace Aula03.Controllers
{
    public class Result()
    {
        public string? ganhador { get; set; }
    }

    public class JogoVelhaController : Controller
    {
        private readonly ILogger<JogoVelhaController> _logger;

        public JogoVelhaController(ILogger<JogoVelhaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Index", new Result());
        }

        [HttpPost]
        public IActionResult Index(
            string A00, string A01, string A02,
            string A10, string A11, string A12,
            string A20, string A21, string A22
            )
        {
            Result resultado = new();

            string vencedor = string.Empty;

            string[,] matrixJV = new string[3, 3];
            int pontuacaoX = 0;
            int pontuacaoO = 0;

            matrixJV[0, 0] = A00;
            matrixJV[0, 1] = A01;
            matrixJV[0, 2] = A02;

            matrixJV[1, 0] = A10;
            matrixJV[1, 1] = A11;
            matrixJV[1, 2] = A12;

            matrixJV[2, 0] = A20;
            matrixJV[2, 1] = A21;
            matrixJV[2, 2] = A22;

            if ((matrixJV[0, 0] == "X" && matrixJV[0, 1] == "X" && matrixJV[0, 2] == "X") || matrixJV[0, 0] == "X" && matrixJV[1, 0] == "X" && matrixJV[2, 0] == "X")
            {
                resultado.ganhador = "O ganhador é  X";
            }
            else if ((matrixJV[1, 0] == "X" && matrixJV[1, 1] == "X" && matrixJV[1, 2] == "X") || matrixJV[0, 1] == "X" && matrixJV[1, 1] == "X" && matrixJV[2, 1] == "X")
            {
                resultado.ganhador = "O ganhador é X";
            }
            else if ((matrixJV[2, 0] == "X" && matrixJV[2, 1] == "X" && matrixJV[2, 2] == "X") || matrixJV[0, 2] == "X" && matrixJV[1, 2] == "X" && matrixJV[2, 2] == "X")
            {
                resultado.ganhador = "O ganhador é X";
            }
            else if ((matrixJV[0, 0] == "O" && matrixJV[0, 1] == "O" && matrixJV[0, 2] == "O") || matrixJV[0, 0] == "O" && matrixJV[1, 0] == "O" && matrixJV[2, 0] == "O")
            {
                resultado.ganhador = "O ganhador é O";
            }
            else if ((matrixJV[1, 0] == "O" && matrixJV[1, 1] == "O" && matrixJV[1, 2] == "O") || matrixJV[0, 1] == "O" && matrixJV[1, 1] == "O" && matrixJV[2, 1] == "O")
            {
                resultado.ganhador = "O ganhador é O";
            }
            else if ((matrixJV[2, 0] == "O" && matrixJV[2, 1] == "O" && matrixJV[2, 2] == "O") || matrixJV[0, 2] == "O" && matrixJV[1, 2] == "O" && matrixJV[2, 2] == "O")
            {
                resultado.ganhador = "O ganhador é O";
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (i + j == 2 && matrixJV[i, j] == "X")
                        {
                            pontuacaoX += 1;
                        }
                        else if (i + j == 2 && matrixJV[i, j] == "X")
                        {
                            pontuacaoO += 1;
                        }

                        if (i == j && matrixJV[i, j] == "X")
                        {
                            pontuacaoX += 1;
                        }
                        else if (i == j && matrixJV[i, j] == "O")
                        {
                            pontuacaoO += 1;
                        }

                        if (pontuacaoX == 3)
                        {
                            resultado.ganhador = "O ganhador é X";
                            break;
                        }
                        else if (pontuacaoO == 3)
                        {
                            resultado.ganhador = @"O ganhador é O";
                        }
                    }
                }
            }
            return View("Index", resultado);
        }
    }
}