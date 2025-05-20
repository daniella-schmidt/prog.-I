namespace Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Adress? HomeAddress { get; set; }
        public Adress? WorkAdress { get; set; }

        public static int InstanceCount = 0;
        public int ObjectCount = 0;
        public bool Validate(){
            return true;
        }

    }
}
