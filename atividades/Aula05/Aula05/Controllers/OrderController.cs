using Aula05.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

namespace Aula05.Controllers
{
    public class OrderController : Controller
    {
        private readonly IWebHostEnvironment environment;
        private readonly OrderRepository _orderRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly ProductRepository _productRepository;

        public OrderController(IWebHostEnvironment environment)
        {
            _orderRepository = new OrderRepository();
            _customerRepository = new CustomerRepository();
            _productRepository = new ProductRepository();
            this.environment = environment;
        }

        [HttpGet]
        public IActionResult Index() => View(_orderRepository.RetrieveAll());

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new OrderViewModel
            {
                Customers = _customerRepository.RetrieveAll() ?? new List<Customer>(),
                SelectedItems = new List<SelectedItem>()
            };

            var products = _productRepository.RetrieveAll() ?? new List<Product>();

            foreach (var product in products)
            {
                viewModel.SelectedItems.Add(new SelectedItem()
                {
                    OrderItem = new OrderItem()
                    {
                        Product = product ?? new Product(),
                        Quantity = 1
                    }
                });
            }

            return View(viewModel);
        }
    }
}