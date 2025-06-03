using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;
using System.IO;

namespace Aula05.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IWebHostEnvironment environment;
        private CustomerRepository _customerRepository;
        
        ///metodo construtor retorna um objeto do tipo do construtor
        public CustomerController(IWebHostEnvironment enviroment)
        {
           _customerRepository = new CustomerRepository();
            this.environment = enviroment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Customer> customers = _customerRepository.RetrieveAll();
            return View(customers);
        }
        [HttpPost]
        public IActionResult Create(Customer c)
        {
            List<Customer> customers = _customerRepository.RetrieveAll();
            _customerRepository.Save(c);
            return View("Index", customers);
        }
        [HttpGet]
        public IActionResult Create()
        { 
            return View();
        }
        [HttpGet]
        public IActionResult ExportDelimitadedFile()
        {
            string fileContent = string.Empty;
            foreach (Customer c in CustomerData.Customers)
            {
                fileContent += 
                    $" {c.Id};{c.Name};{c.HomeAddress!.Id};{c.HomeAddress.City};{c.HomeAddress.State_Province};{c.HomeAddress.Country};{c.HomeAddress.Street1};{c.HomeAddress.Street2};{c.HomeAddress.Postal_Code};{c.HomeAddress.Adress_Type}\n";
            }

            var path = Path.Combine
                (
                    environment.WebRootPath,
                    "TextFiles"
                );

            if (!System.IO.Directory.Exists(path))
            { 
                System.IO.Directory.CreateDirectory(path);
            }

            var filepath = Path.Combine
                (
                    environment.WebRootPath,
                    path,
                    "Clientes.txt"
                );

            if (!System.IO.File.Exists(filepath))//verifica se o arquivo já existe
            {
                //sempre que estiver usando o IO precusa de limpeza de memoria. o using tbm pode ser usado para gerenciar o depouse da memoria
                // por ser IO o using identifica que precisa realizar o depouse (limpeza de memoria)
                using (StreamWriter sw = System.IO.File.CreateText(filepath))
                {
                    sw.Write(fileContent);
                } 
            }

            return View();
        }
    }
}
