using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;
using System.Text;

namespace Imobiliaria.Controllers
{
    public class ClientController : Controller
    {
        private readonly PropertyRepository _propertyRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly ClientRepository _clientRepository;

        public ClientController(PropertyRepository propertyRepository,
                                CategoryRepository categoryRepository,
                                ClientRepository clientRepository)
        {
            _propertyRepository = propertyRepository;
            _categoryRepository = categoryRepository;
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                _clientRepository.Save(client);
                return RedirectToAction("Index");
            }
            return View(client);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var clients = _clientRepository.RetrieveAll();
            return View(clients);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var client = _clientRepository.Retrieve(id);
            if (client == null)
                return NotFound();

            return View(client);
        }

        [HttpPost]
        public IActionResult Update(Client client)
        {
            if (ModelState.IsValid)
            {
                _clientRepository.Update(client);
                return RedirectToAction("Index");
            }
            return View(client);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var client = _clientRepository.Retrieve(id);
            if (client == null)
                return NotFound();

            return View(client);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            _clientRepository.DeleteById(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var client = _clientRepository.Retrieve(id);
            if (client == null)
                return NotFound();

            return View(client);
        }

        [HttpGet]
        public IActionResult Filter(int? categoryId, bool? forSale)
        {
            List<Property> properties;

            if (categoryId.HasValue && categoryId > 0)
            {
                properties = _propertyRepository.RetrieveByCategory(categoryId.Value);
            }
            else
            {
                properties = _propertyRepository.RetrieveAll();
            }

            if (forSale.HasValue)
            {
                properties = properties.Where(p => p.ForSale == forSale.Value).ToList();
            }

            ViewBag.Categories = _categoryRepository.RetrieveAll();
            ViewBag.SelectedCategory = categoryId.HasValue ?
                _categoryRepository.Retrieve(categoryId.Value)?.Name : "All";
            ViewBag.FilterType = forSale.HasValue ?
                (forSale.Value ? "Buy" : "Rent") : "All";

            return View("PropertiesList", properties);
        }

        [HttpGet]
        public IActionResult PropertyDetails(int id)
        {
            var property = _propertyRepository.Retrieve(id);
            if (property == null) return NotFound();

            return View(property);
        }

        [HttpPost]
        public IActionResult AddInterest(int clientId, int propertyId)
        {
            _clientRepository.AddInterest(clientId, propertyId);
            TempData["Success"] = "Interest registered! We will contact you.";
            return RedirectToAction("PropertyDetails", new { id = propertyId });
        }

        [HttpGet]
        public IActionResult MyInterests(int clientId)
        {
            var client = _clientRepository.Retrieve(clientId);
            if (client == null) return NotFound();

            return View(client.InterestedProperties ?? new List<Property>());
        }

        [HttpGet]
        public IActionResult ExportDelimitedFileWithSave()
        {
            var content = CreateDelimitedContent();
            var bytes = Encoding.UTF8.GetBytes(content);
            return File(bytes, "text/csv", "ClientsDelimited.txt");
        }

        [HttpGet]
        public IActionResult ExportFixedFileWithSave()
        {
            var content = CreateFixedContent();
            var bytes = Encoding.UTF8.GetBytes(content);
            return File(bytes, "text/plain", "ClientsFixed.txt");
        }

        private string CreateDelimitedContent()
        {
            var sb = new StringBuilder();

            var clients = _clientRepository.RetrieveAll();
            var allProperties = _propertyRepository.RetrieveAll();

            var propertiesByClient = allProperties
                .Where(p => p.BuyerClient != null)
                .GroupBy(p => p.BuyerClient!.Id)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var client in clients)
            {
                var clientProperties = propertiesByClient.GetValueOrDefault(client.Id, new List<Property>());
                var purchasedCount = clientProperties.Count(p => p.ForSale);
                var rentedCount = clientProperties.Count(p => !p.ForSale);

                sb.AppendLine($"{client.Id};{client.Name};{client.Email};{client.Phone};{client.CPF};" +
                              $"{client.InterestedProperties?.Count ?? 0};" +
                              $"{purchasedCount};" +
                              $"{rentedCount}");
            }

            return sb.ToString();
        }

        private string CreateFixedContent()
        {
            var sb = new StringBuilder();

            var clients = _clientRepository.RetrieveAll();
            var allProperties = _propertyRepository.RetrieveAll();

            var propertiesByClient = allProperties
                .Where(p => p.BuyerClient != null)
                .GroupBy(p => p.BuyerClient!.Id)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var client in clients)
            {
                var clientProperties = propertiesByClient.GetValueOrDefault(client.Id, new List<Property>());
                var purchasedCount = clientProperties.Count(p => p.ForSale);
                var rentedCount = clientProperties.Count(p => !p.ForSale);

                var truncatedName = client.Name?.Length > 20 ? client.Name.Substring(0, 17) + "..." : client.Name ?? "";
                var truncatedEmail = client.Email?.Length > 20 ? client.Email.Substring(0, 17) + "..." : client.Email ?? "";

                sb.AppendFormat("{0,-4} {1,-20} {2,-20} {3,-12} {4,-12} {5,-9} {6,-10} {7,-8}\r\n",
                    client.Id,
                    truncatedName,
                    truncatedEmail,
                    client.Phone ?? "",
                    client.CPF ?? "",
                    client.InterestedProperties?.Count ?? 0,
                    purchasedCount,
                    rentedCount);
            }

            return sb.ToString();
        }
    }
}