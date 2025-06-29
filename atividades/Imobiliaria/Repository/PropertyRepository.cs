using Model;
using System.Text;

namespace Repository
{
    public class PropertyRepository
    {
        public List<Property> _properties => PropertyData.Properties;

        public Property Retrieve(int id)
        {
            return _properties.FirstOrDefault(p => p.Id == id)!;
        }

        public List<Property> RetrieveByName(string name)
        {
            return _properties
                .Where(p => p.Name?.ToLower().Contains(name.ToLower()) ?? false)
                .ToList();
        }

        public List<Property> RetrieveByCategory(int categoryId)
        {
            return _properties
                .Where(p => p.Category?.Id == categoryId)
                .ToList();
        }

        public List<Property> RetrieveByType(bool forSale)
        {
            return _properties
                .Where(p => p.ForSale == forSale)
                .ToList();
        }

        public List<Property> RetrieveAll()
        {
            return _properties;
        }

        public void Save(Property property)
        {
            property.Id = _properties.Any() ? _properties.Max(p => p.Id) + 1 : 1;
            _properties.Add(property);
        }

        public bool Delete(Property property)
        {
            return _properties.Remove(property);
        }

        public bool DeleteById(int id)
        {
            var property = Retrieve(id);
            return property != null && Delete(property);
        }

        public void Update(Property newProperty)
        {
            var oldProperty = Retrieve(newProperty.Id);
            if (oldProperty != null)
            {
                oldProperty.Name = newProperty.Name;
                oldProperty.Number = newProperty.Number;
                oldProperty.Description = newProperty.Description;
                oldProperty.BedroomCount = newProperty.BedroomCount;
                oldProperty.GarageSpots = newProperty.GarageSpots;
                oldProperty.BathroomCount = newProperty.BathroomCount;
                oldProperty.Address = newProperty.Address;
                oldProperty.Category = newProperty.Category;
                oldProperty.Price = newProperty.Price;
                oldProperty.ForSale = newProperty.ForSale;
            }
        }

        public int GetCount() => _properties.Count;

        public Dictionary<string, int> GetStatistics()
        {
            var stats = new Dictionary<string, int>();

            stats["Total"] = _properties.Count;

            stats["Available For Sale"] = _properties.Count(p => p.ForSale && !p.SoldOrRented);

            stats["Available For Rent"] = _properties.Count(p => !p.ForSale && !p.SoldOrRented);

            stats["Sold or Rented"] = _properties.Count(p => p.SoldOrRented);

            foreach (var category in PropertyData.Categories)
            {
                stats[$"{category.Name}"] = _properties.Count(p => p.Category?.Id == category.Id);
            }

            return stats;
        }


        public void AddClientInterest(int propertyId, int clientId)
        {
            var property = Retrieve(propertyId);
            var client = new ClientRepository().Retrieve(clientId);

            if (property != null && client != null)
            {
                property.InterestedClients ??= new List<Client>();
                if (!property.InterestedClients.Any(c => c.Id == clientId))
                {
                    property.InterestedClients.Add(client);
                    property.InterestDate = DateTime.Now;
                }
            }
        }

        public void RegisterSaleOrRental(int propertyId, int clientId, bool sold)
        {
            var property = Retrieve(propertyId);
            var client = new ClientRepository().Retrieve(clientId);

            if (property != null && client != null)
            {
                property.BuyerClient = client;
                property.SoldOrRented = sold;
                property.SaleOrRentalDate = DateTime.Now;

                property.InterestedClients?.RemoveAll(c => c.Id == clientId);
            }
        }
    }
}
