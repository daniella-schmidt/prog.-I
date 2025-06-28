using Model;

namespace Repository
{
    public class CategoryRepository
    {
        public CategoryRepository()
        {
            PropertyData.Categories ??= new List<Category>();
        }

        public Category Retrieve(int id)
        {
            foreach (Category c in PropertyData.Categories)
            {
                if (c.Id == id)
                    return c;
            }
            return null!;
        }

        public List<Category> RetrieveByName(string name)
        {
            List<Category> result = new List<Category>();

            foreach (Category c in PropertyData.Categories)
            {
                if (c.Name!.ToLower().Contains(name.ToLower()))
                {
                    result.Add(c);
                }
            }
            return result;
        }

        public List<Category> RetrieveAll()
        {
            return PropertyData.Categories;
        }

        public void Save(Category category)
        {
            category.Id = GetCount() + 1;
            PropertyData.Categories.Add(category);
        }

        public bool Delete(Category category)
        {
            return PropertyData.Categories.Remove(category);
        }

        public bool DeleteById(int id)
        {
            Category category = Retrieve(id);
            if (category != null)
                return Delete(category);
            return false;
        }

        public void Update(Category newCategory)
        {
            Category oldCategory = Retrieve(newCategory.Id);
            if (oldCategory != null)
            {
                oldCategory.Name = newCategory.Name;
                oldCategory.Description = newCategory.Description;
            }
        }

        public int GetCount()
        {
            return PropertyData.Categories.Count;
        }

        public void InitializeDefaultCategories()
        {
            if (PropertyData.Categories.Count == 0)
            {
                Save(new Category { Name = "Apartamento", Description = "Unidade residencial em edifício" });
                Save(new Category { Name = "Casa", Description = "Residência térrea ou sobrado" });
                Save(new Category { Name = "Sítio", Description = "Propriedade rural para lazer" });
                Save(new Category { Name = "Sala Comercial", Description = "Espaço comercial para negócios" });
            }
        }

    }
}