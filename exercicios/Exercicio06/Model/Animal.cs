namespace Model
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BornDate { get; set; } 
        public string Color { get; set; }
        public float Weight { get; set; }
        public double Height { get; set; }
        public TypeAnimal TypeAnimal { get; set; } 
        public List<Vaccine> Vaccine { get; } = new(); 

        public Animal(string name, DateTime bornDate, TypeAnimal typeAnimal)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            BornDate = bornDate;
            TypeAnimal = typeAnimal ?? throw new ArgumentNullException(nameof(typeAnimal));
        }

        public int CalculateAge()
        {
            var today = DateTime.Today;
            int age = today.Year - BornDate.Year;
            if (BornDate.Date > today.AddYears(-age)) age--;
            return age;
        }

        public void AddVaccine(Vaccine vaccine)
        {
            if (vaccine.Animal != this)
                throw new ArgumentException("Vaccine does not belong to this animal");

            Vaccine.Add(vaccine);
        }

        public override string ToString() =>
            $"{Name} ({TypeAnimal}) - Age: {CalculateAge()} years";
    }
}