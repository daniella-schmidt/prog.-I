namespace Model
{
    public class Vaccine 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateAdministered { get; set; } = DateTime.Now;
        public DateTime ExpirationDate { get; set; }
        public Animal Animal { get; set; } 

        public bool IsValid() => DateTime.Now < ExpirationDate;

        public Vaccine(string name, Animal animal, DateTime expirationDate)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Animal = animal ?? throw new ArgumentNullException(nameof(animal));
            ExpirationDate = expirationDate;
        }
    }
}