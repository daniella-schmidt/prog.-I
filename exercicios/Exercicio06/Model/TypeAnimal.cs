namespace Model
{
    public class TypeAnimal
    {
        public int Id { get; set; }
        public string Name { get; set; } 

        public TypeAnimal(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public override string ToString() => Name;
    }
}