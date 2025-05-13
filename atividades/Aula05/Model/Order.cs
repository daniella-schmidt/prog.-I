namespace Model
{
    public class Order
    {
        public int Id { get; set; }
        public Customer? Costumer { get; set; }
        public DateTime OrderDate { get; set; }
        public string? ShippingAdress { get; set; }
        public List<OrderItem>? OrderItems { get; set; }

        public bool Validate()
        {
            return true;
        }
        public Order Retrieve()
        {
            return new Order();
        }
        //1° snakecase depois camal
        //primeiro o tipo depois a variavel
        public void Save(Order order)
        {

        }
    }
}
