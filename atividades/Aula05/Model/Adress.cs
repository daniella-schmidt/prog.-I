namespace Model
{
    public class Adress
    {
        public int Id { get; set; }
        public string? Street1 { get; set; }
        public string? Street2 { get; set; }
        public string? City { get; set; }
        public string? State_Province { get; set; }
        public string? Postal_Code { get; set; }
        public string? Country { get; set; }
        public string? Adress_Type { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
