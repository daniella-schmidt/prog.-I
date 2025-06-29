using System.Net;

namespace Model
{
    public class Property
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int BedroomCount { get; set; }
        public int GarageSpots { get; set; }
        public int BathroomCount { get; set; }
        public Address? Address { get; set; }
        public Category? Category { get; set; }
        public decimal Price { get; set; }
        public bool ForSale { get; set; } = true;

        public List<Client>? InterestedClients { get; set; } = new List<Client>();
        public Client? BuyerClient { get; set; }
        public DateTime? InterestDate { get; set; }
        public DateTime? SaleOrRentalDate { get; set; }
        public bool SoldOrRented { get; set; }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(this.Name) &&
                   this.Price > 0 &&
                   this.Address != null &&
                   !string.IsNullOrEmpty(this.Address.Street) &&
                   this.Category?.Id > 0;
        }

        public string ToDelimitedString(string delimiter = ";")
        {
            return $"{Id}{delimiter}" +
                   $"{Name}{delimiter}" +
                   $"{Description}{delimiter}" +
                   $"{BedroomCount}{delimiter}" +
                   $"{GarageSpots}{delimiter}" +
                   $"{BathroomCount}{delimiter}" +
                   $"{Address?.Street}, {Address?.City}, {Address?.State_Province}{delimiter}" +
                   $"{Category?.Name}{delimiter}" +
                   $"{Price:C}{delimiter}" +
                   $"{(ForSale ? "For Sale" : "For Rent")}";
        }
    }
}
