
namespace GraphQLDemo.API.Schema
{
    public class Mutation
    {
        private List<CategoryType> _categories;

        public Mutation()
        {
            _categories = new List<CategoryType>();
        }

        public bool CreateCategory(string name, string description)
        {
            var category = new CategoryType()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description
            };
            _categories.Add(category);
            return true;
        }

        public bool UpdateCategory(Guid id, string name, string description)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                throw new Exception("Category not found");
            category.Name = name;
            category.Description = description;

            return true;
        }

        public bool DeleteCategory(Guid id)
        {
            return _categories.RemoveAll(x => x.Id == id) >= 1;
        }
    }
}
