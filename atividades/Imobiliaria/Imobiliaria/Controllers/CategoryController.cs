using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

namespace Imobiliaria.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly PropertyRepository _propertyRepository;

        public CategoryController(CategoryRepository categoryRepository,
                                  PropertyRepository propertyRepository)
        {
            _categoryRepository = categoryRepository;
            _propertyRepository = propertyRepository;
            _categoryRepository.InitializeDefaultCategories();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categories = _categoryRepository.RetrieveAll();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid && category.Validate())
            {
                _categoryRepository.Save(category);
                TempData["Success"] = "Category successfully created!";
                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            Category category = _categoryRepository.Retrieve(id.Value);

            if (category == null)
                return NotFound();

            var properties = _propertyRepository.RetrieveByCategory(id.Value);
            ViewBag.Properties = properties;
            ViewBag.TotalProperties = properties.Count;

            return View(category);
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id is null || id <= 0)
                return NotFound();

            Category category = _categoryRepository.Retrieve(id.Value);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (!ModelState.IsValid || !category.Validate())
                return View(category);

            _categoryRepository.Update(category);
            TempData["Success"] = "Category successfully updated!";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            Category category = _categoryRepository.Retrieve(id.Value);

            if (category == null)
                return NotFound();

            var linkedProperties = _propertyRepository.RetrieveByCategory(id.Value);
            ViewBag.LinkedProperties = linkedProperties;
            ViewBag.CanDelete = linkedProperties.Count == 0;

            return View(category);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            var linkedProperties = _propertyRepository.RetrieveByCategory(id.Value);

            if (linkedProperties.Count > 0)
            {
                TempData["Error"] = $"Cannot delete this category because there are {linkedProperties.Count} linked property(ies).";
                return RedirectToAction("Delete", new { id });
            }

            if (!_categoryRepository.DeleteById(id.Value))
                return NotFound();

            TempData["Success"] = "Category successfully deleted!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Search(string name)
        {
            List<Category> categories;

            if (!string.IsNullOrEmpty(name))
            {
                categories = _categoryRepository.RetrieveByName(name);
                ViewBag.SearchTerm = name;
            }
            else
            {
                categories = _categoryRepository.RetrieveAll();
            }

            return View("Index", categories);
        }

        [HttpGet]
        public IActionResult GetPropertiesByCategory(int categoryId)
        {
            var category = _categoryRepository.Retrieve(categoryId);
            if (category == null)
                return NotFound();

            var properties = _propertyRepository.RetrieveByCategory(categoryId);

            ViewBag.CategoryName = category.Name;
            ViewBag.CategoryId = categoryId;

            return View(properties);
        }
    }
}
