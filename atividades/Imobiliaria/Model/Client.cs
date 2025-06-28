namespace Model
{
    public class Client
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? CPF { get; set; }
        public List<Property>? InterestedProperties { get; set; } = new List<Property>(); 

        public bool Validate()
        {
            return
                Id > 0 &&
                !string.IsNullOrEmpty(Name) &&
                !string.IsNullOrEmpty(Email) &&
                !string.IsNullOrEmpty(Phone) &&
                !string.IsNullOrEmpty(CPF);
        }

        public string ToDelimitedString(string delimiter = ";")
        {
            return $"{Id}{delimiter}" +
                   $"{Name}{delimiter}" +
                   $"{Email}{delimiter}" +
                   $"{Phone}{delimiter}" +
                   $"{CPF}{delimiter}" +
                   $"Interests: {InterestedProperties?.Count ?? 0}";
        }

        public int GetPurchasedPropertiesCount(List<Property> allProperties)
        {
            return allProperties.Count(p => p.BuyerClient?.Id == this.Id && p.ForSale);
        }

        public int GetRentedPropertiesCount(List<Property> allProperties)
        {
            return allProperties.Count(p => p.BuyerClient?.Id == this.Id && !p.ForSale);
        }
    }
}
