using Model;

namespace Aula05.ViewModels
{
    public class OrderViewModel
    {
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public int? CustomerId { get; set; }
        public List<SelectedItem> SelectedItems { get; set; } = new List<SelectedItem>();
    }

    public class SelectedItem
    {
        public bool IsSelected { get; set; } = false;
        public OrderItem OrderItem { get; set; } = new OrderItem();
    }
}