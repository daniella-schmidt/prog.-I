namespace Model
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Product? Product { get; set; }
        public double Quantity { get; set; }
        public double PurchasePrice { get; set; }

        public bool Validate()
        {
            bool isValid = true;
            isValid =
                //this, basicamente um operador para não deixar ambiguio. é útil para diferenciar variáveis locais de atributos
                //da classe que o validate está inserido
                // neste caso é usado por pratica entre pr'ogramadores, mas não se faz util aqui
                (this.Id > 0) &&
                Product != null &&
                (Quantity > 0) &&
                (PurchasePrice > 0);
               
            return isValid;
        }
    }
}
