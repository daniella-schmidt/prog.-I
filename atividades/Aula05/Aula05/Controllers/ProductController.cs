using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;
using System.IO;

public class ProductController : Controller
{
    private readonly IWebHostEnvironment _environment;
    private readonly ProductRepository _repository;

    public ProductController(IWebHostEnvironment environment)
    {
        _repository = new ProductRepository();
        _environment = environment;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var products = _repository.RetrieveAll();
        return View(products);
    }

    [HttpGet]
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
        foreach (Product p in ProductData.Products)
        {
            fileContent += $"{p.Id};{p.ProductName};{p.Description};{p.CurrentPrice}\n";
        }

        var path = Path.Combine(_environment.WebRootPath, "TextFiles");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var filepath = Path.Combine(path, "Produtos.txt");

        System.IO.File.WriteAllText(filepath, fileContent);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id is null || id.Value <= 0)
            return NotFound();

        Product product = _repository.Retrieve(id.Value);

        if (product == null)
            return NotFound();

        return View(product);
    }

    [HttpPost]
    public IActionResult ConfirmDelete(int? id)
    {
        if (id is null || id.Value <= 0)
            return NotFound();

        if (!_repository.DeleteById(id.Value))
            return NotFound();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Update(int? id)
    {
        if (id is null || id <= 0)
            return NotFound();

        Product product = _repository.Retrieve(id.Value);

        if (product == null)
            return NotFound();

        return View(product);
    }

    [HttpPost]
    public IActionResult Update(Product product)
    {
        if (!ModelState.IsValid)
            return View(product);

        _repository.Update(product);

        var products = _repository.RetrieveAll();
        return View("Index", products);
    }
}