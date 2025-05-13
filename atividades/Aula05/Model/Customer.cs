namespace Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? HomeAddress { get; set; }
        public string? WorkAdress { get; set; }

        public bool Validate(){
            return true;
        }
        public Customer Retrieve() { 
            return new Customer();
        }
        public void Save(Customer customer) { 
            
        }

    }
}
