namespace Model
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public bool Validate()
        {
            bool isValid = true;
            isValid =
                (this.Id > 0) &&
                !string.IsNullOrEmpty(this.Name);
            return isValid;
        }
    }
}
