using Model;

namespace Repository
{
    public class ProductRepository
    {
        public ProductRepository() { }

        public Product Retrieve(int id)
        {
            foreach (Product c in ProductData.Products)
            {
                if (c.Id == id)
                    return c;
            }
            return null!;
        }
        public List<Product> RetrieveByName(string name)
        {
            List<Product> ret = new List<Product>();

            foreach (Product c in ProductData.Products)
            {
                if (c.ProductName!.ToLower().Contains(name.ToLower()))
                {
                    ret.Add(c);
                }
            }
            return ret;
        }
        public List<Product> RetrieveAll()
        {
            return ProductData.Products;
        }
        public void Save(Product customer)
        {
            customer.Id = GetCount() + 1;
            ProductData.Products.Add(customer);
        }
        public bool Delete(Product customer)
        {
            return ProductData.Products.Remove(customer);

        }
        public bool DeleteById(int id)
        {
            Product customer = Retrieve(id);
            if (customer != null)
                return Delete(customer);
            return false;
        }
        public void Update(Product newProduct)
        {
            Product oldProduct = Retrieve(newProduct.Id);
            oldProduct.ProductName = newProduct.ProductName;
            oldProduct.Description = newProduct.Description;
            oldProduct.CurrentPrice = newProduct.CurrentPrice;
        }
        public int GetCount()
        {
            return ProductData.Products.Count;
        }
    }
}
