using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

namespace Aula05.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerRepository _customerRepository;
        public CustomerController()
        {
           _customerRepository = new CustomerRepository();
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
    }

}
