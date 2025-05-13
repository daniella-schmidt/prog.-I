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
            return true;
        }
        public OrderItem Retrieve()
        {
            return new OrderItem();
        }
        //1° snakecase depois camal
        //primeiro o tipo depois a variavel
        public void Save(OrderItem orderItem)
        {

        }
    }
}
