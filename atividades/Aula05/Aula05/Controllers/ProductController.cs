using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

public class ProductController : Controller
{
    private readonly ProductRepository _repository = new ProductRepository();

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
}