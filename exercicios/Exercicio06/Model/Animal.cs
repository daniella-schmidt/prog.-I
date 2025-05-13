namespace Model
{
    public class Animal
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? BornDate { get; set; }
        public string? Color { get; set; }
        public float Weight { get; set; }
        public double Height { get; set; }
        public TypeAnimal? TypeAnimal { get; set; }

    }
}
