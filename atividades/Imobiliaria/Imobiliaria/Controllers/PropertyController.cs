using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;
using System.IO;

namespace Imobiliaria.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly PropertyRepository _propertyRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly ClientRepository _clientRepository;

        public PropertyController(IWebHostEnvironment environment,
                                  PropertyRepository propertyRepository,
                                  CategoryRepository categoryRepository,
                                  ClientRepository clientRepository)
        {
            _environment = environment;
            _propertyRepository = propertyRepository;
            _categoryRepository = categoryRepository;
            _clientRepository = clientRepository;
        }

        private void InitializeViewBag()
        {
            ViewBag.Categories = _categoryRepository.RetrieveAll() ?? new List<Category>();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var properties = _propertyRepository.RetrieveAll();
            ViewBag.Statistics = _propertyRepository.GetStatistics();
            InitializeViewBag();
            return View(properties);
        }

        [HttpGet]
        public IActionResult Create()
        {
            InitializeViewBag();
            return View(new Property
            {
                Address = new Address(),
                Category = new Category()
            });
        }

        [HttpPost]
        public IActionResult Create(Property property)
        {
            if (!ModelState.IsValid)
            {
                InitializeViewBag();
                return View(property);
            }

            if (property.Category?.Id > 0)
            {
                property.Category = _categoryRepository.Retrieve(property.Category.Id);
            }
            else
            {
                ModelState.AddModelError("Category.Id", "Selecione uma categoria válida");
                InitializeViewBag();
                return View(property);
            }

            if (!property.Validate())
            {
                InitializeViewBag();
                return View(property);
            }

            _propertyRepository.Save(property);
            TempData["Success"] = "Property successfully registered!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ExportDelimitedFile()
        {
            try
            {
                var content = CreateDelimitedFileContent();
                if (SaveFile(content, "Properties.txt"))
                {
                    TempData["Success"] = "File successfully exported!";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Export error: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            var property = _propertyRepository.Retrieve(id.Value);
            if (property == null)
                return NotFound();

            if (property.Category != null)
            {
                property.Category = _categoryRepository.Retrieve(property.Category.Id);
            }

            ViewBag.Clients = _clientRepository.RetrieveAll();

            return View(property);
        }

        [HttpPost]
        public IActionResult AddInterest(int propertyId, int clientId)
        {
            _propertyRepository.AddClientInterest(propertyId, clientId);
            TempData["Success"] = "Client interest successfully registered!";
            return RedirectToAction("Details", new { id = propertyId });
        }

        [HttpPost]
        public IActionResult RegisterSaleRental(int propertyId, int clientId, bool sold)
        {
            _propertyRepository.RegisterSaleOrRental(propertyId, clientId, sold);
            TempData["Success"] = $"Property {(sold ? "sold" : "rented")} successfully!";
            return RedirectToAction("Details", new { id = propertyId });
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id is null || id <= 0)
                return NotFound();

            var property = _propertyRepository.Retrieve(id.Value);
            if (property == null)
                return NotFound();

            property.Address ??= new Address();
            property.Category ??= new Category();

            ViewBag.Categories = _categoryRepository.RetrieveAll();
            return View(property);
        }

        [HttpPost]
        public IActionResult Update(Property property)
        {
            if (!ModelState.IsValid || !property.Validate())
            {
                ViewBag.Categories = _categoryRepository.RetrieveAll();
                return View(property);
            }

            if (property.Category?.Id > 0)
            {
                property.Category = _categoryRepository.Retrieve(property.Category.Id);
            }

            _propertyRepository.Update(property);
            TempData["Success"] = "Property successfully updated!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            var property = _propertyRepository.Retrieve(id.Value);
            if (property == null)
                return NotFound();

            return View(property);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            if (!_propertyRepository.DeleteById(id.Value))
                return NotFound();

            TempData["Success"] = "Property successfully deleted!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ExportFixedFile()
        {
            try
            {
                var content = CreateFixedFileContent();
                var bytes = System.Text.Encoding.UTF8.GetBytes(content);
                return new FileContentResult(bytes, "text/plain") { FileDownloadName = "PropertiesFixedLength.txt" };
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Export error: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult ExportDelimitedFileWithSave()
        {
            try
            {
                var content = CreateDelimitedFileContent();
                var bytes = System.Text.Encoding.UTF8.GetBytes(content);

                if (SaveFile(content, "Properties.txt"))
                {
                    TempData["Success"] = "File successfully exported! Check the TextFiles folder.";
                }
                else
                {
                    TempData["Error"] = "Error exporting file.";
                }

                return new FileContentResult(bytes, "text/plain") { FileDownloadName = "PropertiesDelimited.txt" };
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Export error: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult ExportFixedFileWithSave()
        {
            try
            {
                var content = CreateFixedFileContent();
                var bytes = System.Text.Encoding.UTF8.GetBytes(content);

                if (SaveFile(content, "PropertiesFixedLength.txt"))
                {
                    TempData["Success"] = "Fixed-length file exported successfully!";
                }
                else
                {
                    TempData["Error"] = "Error exporting fixed-length file.";
                }

                return new FileContentResult(bytes, "text/plain") { FileDownloadName = "PropertiesFixed.txt" };
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Export error: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult FilterByCategory(int? categoryId)
        {
            List<Property> properties;

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                properties = _propertyRepository.RetrieveByCategory(categoryId.Value);
                ViewBag.SelectedCategory = _categoryRepository.Retrieve(categoryId.Value)?.Name;
            }
            else
            {
                properties = _propertyRepository.RetrieveAll();
            }

            ViewBag.Categories = _categoryRepository.RetrieveAll();
            ViewBag.Statistics = _propertyRepository.GetStatistics();

            return View("Index", properties);
        }

        [HttpGet]
        public IActionResult FilterByType(bool? forSale)
        {
            List<Property> properties;

            if (forSale.HasValue)
            {
                properties = _propertyRepository.RetrieveByType(forSale.Value);
                ViewBag.FilterType = forSale.Value ? "Sale" : "Rent";
            }
            else
            {
                properties = _propertyRepository.RetrieveAll();
            }

            ViewBag.Categories = _categoryRepository.RetrieveAll();
            ViewBag.Statistics = _propertyRepository.GetStatistics();

            return View("Index", properties);
        }

        private string CreateDelimitedFileContent()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("ID;Name;Description;Bedrooms;Garage_Spots;Bathrooms;Address;Category;Value;Type;Status;Interested;Buyer");

            foreach (var property in _propertyRepository.RetrieveAll())
            {
                var address = property.Address != null
                    ? $"{property.Address.Street}, {property.Address.City}, {property.Address.State_Province}"
                    : "Not informed";

                var category = property.Category?.Name ?? "Not informed";
                var type = property.ForSale ? "Sale" : "Rent";

                var status = property.SoldOrRented ? (property.ForSale ? "Sold" : "Rented") : "Available";
                var interested = property.InterestedClients?.Count ?? 0;
                var buyer = property.BuyerClient?.Name ?? "None";

                sb.AppendLine($"{property.Id};{property.Name};{property.Description};{property.BedroomCount};" +
                              $"{property.GarageSpots};{property.BathroomCount};{address};" +
                              $"{category};{property.Price.ToString("F2")};{type};{status};" +
                              $"{interested};{buyer}");
            }

            return sb.ToString();
        }

        private string CreateFixedFileContent()
        {
            var sb = new System.Text.StringBuilder();

            foreach (var property in PropertyData.Properties)
            {
                var address = property.Address != null
                    ? $"{property.Address.Street}, {property.Address.City}"
                    : "Not informed";

                var category = property.Category?.Name ?? "Not informed";
                var type = property.ForSale ? "Sale" : "Rent";

                sb.AppendFormat("{0,-5}{1,-50}{2,-100}{3,-3}{4,-3}{5,-3}{6,-80}{7,-20}{8,-15}{9,-10}\n",
                    property.Id, property.Name, property.Description, property.BedroomCount,
                    property.GarageSpots, property.BathroomCount, address, category,
                    property.Price.ToString("F2"), type);
            }

            return sb.ToString();
        }

        private bool SaveFile(string content, string fileName)
        {
            if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(fileName))
                return false;

            var path = Path.Combine(_environment.WebRootPath, "TextFiles");

            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var filePath = Path.Combine(path, fileName);
                System.IO.File.WriteAllText(filePath, content);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
