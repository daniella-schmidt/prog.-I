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
        
        //metodo construtor retorna um objeto do tipo do construtor
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

            SaveFile(fileContent, "DelimitatedFile.txt");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ExportFixedFile()
        {
            string fileContent = string.Empty;
            foreach (Customer c in CustomerData.Customers)
            {
                fileContent +=
                   String.Format("{0:5}", c.Id) +
                   String.Format("{0:127}", c.Name) +
                   String.Format("{0:5}", c.HomeAddress!.Id) +
                   String.Format("{0:32}", c.HomeAddress!.City) +
                   String.Format("{0:2}", c.HomeAddress!.State_Province) +
                   String.Format("{0:32}", c.HomeAddress!.Country) +
                   String.Format("{0:64}", c.HomeAddress!.Street1) +
                   String.Format("{0:64}", c.HomeAddress!.Street2) +
                   String.Format("{0:9}", c.HomeAddress!.Postal_Code) +
                   String.Format("{0:16}", c.HomeAddress!.Adress_Type) + 
                   "\n";
            }

            SaveFile(fileContent, "FixedFile.txt");

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null || id.Value <= 0) 
                return NotFound();

            Customer customer = _customerRepository.Retrieve(id.Value);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id is null || id <= 0)
                return NotFound();

            Customer customer = _customerRepository.Retrieve(id.Value);

            if (customer == null)
                return NotFound();

            customer.HomeAddress ??= new Adress();

            return View(customer);
        }

        [HttpPost]
        public IActionResult Update(Customer c)
        {
            if (!ModelState.IsValid)
                return View(c);

            _customerRepository.Update(c);

            List<Customer> customers = _customerRepository.RetrieveAll();
            return View("Index", customers);
        }


        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            if (!_customerRepository.DeleteById(id.Value))
                return NotFound();

            return RedirectToAction("Index");
        }

        private bool SaveFile(string content, string fileName)
        {
            bool ret = true;
            if(string.IsNullOrEmpty(content) || string.IsNullOrEmpty(fileName))
                return false;

            var path = Path.Combine
                (
                    environment.WebRootPath,
                    "TextFiles"
                );

            try
            {
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                var filepath = Path.Combine
                    (
                        environment.WebRootPath,
                        path,
                        fileName
                    );

                if (!System.IO.File.Exists(filepath))//verifica se o arquivo já existe
                {
                    //sempre que estiver usando o IO precisa de limpeza de memoria. o using tbm pode ser usado para gerenciar o depouse da memoria
                    // por ser IO o using identifica que precisa realizar o depouse (limpeza de memoria)
                    using (StreamWriter sw = System.IO.File.CreateText(filepath))
                    {
                        sw.Write(content);
                    }
                }
            }
            catch (IOException ioEx)
            {
                string msg = ioEx.Message;
                //throw ioEx; -- joga o erro todo na tela do usuario
                ret = false;
            }
            catch (Exception ex)
            { 
                string msg = ex.Message;
                ret = false;
            }

            return ret;
        }
    }
}

