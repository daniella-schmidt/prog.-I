namespace Model
{
    public class Address
    {
        public int Id { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State_Province { get; set; }
        public string? Postal_Code { get; set; }
        public string? Country { get; set; }
        public string? Address_Type { get; set; } 

        public bool Validate()
        {
            bool isValid = true;
            isValid =
                (this.Id > 0) &&
                !string.IsNullOrEmpty(this.Street) &&
                !string.IsNullOrEmpty(this.City) &&
                !string.IsNullOrEmpty(this.State_Province) &&
                !string.IsNullOrEmpty(this.Country);
            return isValid;
        }

        public string GetFormattedAddress()
        {
            var parts = new List<string>();

            if (!string.IsNullOrEmpty(Street))
                parts.Add(Street);

            if (!string.IsNullOrEmpty(City))
                parts.Add(City);

            if (!string.IsNullOrEmpty(State_Province))
                parts.Add(State_Province);

            if (!string.IsNullOrEmpty(Postal_Code))
                parts.Add($"CEP: {Postal_Code}");

            if (!string.IsNullOrEmpty(Country))
                parts.Add(Country);

            return string.Join(", ", parts);
        }

        public string GetShortAddress()
        {
            return $"{Street}, {City} - {State_Province}";
        }
    }
}