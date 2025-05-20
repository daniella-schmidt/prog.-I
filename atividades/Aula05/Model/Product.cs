namespace Model
{
    public class Product
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public float CurrentPrice { get; set; }

        public bool Validate()
        {
            bool isValid = true;
            isValid =
                //this, basicamente um operador para não deixar ambiguio. é útil para diferenciar variáveis locais de atributos da classe
                !string.IsNullOrEmpty(this.ProductName) &&
                (this.Id > 0) && (this.CurrentPrice >0);
            return isValid;
        }
        public Product Retrieve()
        {
            return new Product();
        }
        public void Save(Product product)
        {

        }
    }
}
