    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Repository;

    public class ProductController : Controller
    {
        public readonly IWebHostEnvironment environment;
        private ProductRepository _repository;

        public ProductController(IWebHostEnvironment enviroment)
        {
            _repository = new ProductRepository();
            this.environment = enviroment;
        }

        public IActionResult Index()
        {
            var products = _repository.RetrieveAll();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _repository.Save(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public IActionResult ExportDelimitadedFile()
        {
            string fileContent = string.Empty;
            foreach (Product c in ProductData.Products)
            {
                fileContent += $"{c.Id};{c.ProductName};{c.Description};{c.CurrentPrice}\n";
            }

            var path = Path.Combine(environment.WebRootPath, "TextFiles");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var filepath = Path.Combine(path, "Produtos.txt");

            if (!System.IO.File.Exists(filepath))
            {
                using (StreamWriter sw = System.IO.File.CreateText(filepath))
                {
                    sw.Write(fileContent);
                }
            }

            return View();
        }
    }

