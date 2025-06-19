namespace Model
{
    public class Order
    {
        #region Atributos
        public int Id { get; set; }
        public Customer? Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public Adress? ShippingAdress { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
        #endregion

        //metodo construtor sempre vai retornar ele mesmo
        public Order()
        {
            OrderDate = DateTime.Now;
            OrderItems = new List<OrderItem>();
        }

        //this() referencia o construtor padrao o Order() <-- esse sem parametros
        public Order(int orderId) : this()
        { 
            Id = orderId;

        }
        public bool Validate()
        {
            bool isValid = true;
            isValid =
                 ShippingAdress != null &&
                //this, basicamente um operador para não deixar ambiguio. é útil para diferenciar variáveis locais de atributos da classe
                (this.Id > 0) && OrderItems != null && 
                (OrderItems.Count > 0) &&
                (OrderDate <= DateTime.Now);
            return isValid;
        }
        public List<Order> Retrieve()
        {
            return new List<Order>();
        }
    }
}
