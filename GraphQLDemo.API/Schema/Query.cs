
using Bogus;
using GraphQLDemo.API.Services;

namespace GraphQLDemo.API.Schema
{
    public class Query
    {
        private readonly CategoryRepository categoryRepository;

        public Query(CategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public List<ProductType> GetProductTypes()
        {

            Faker<CategoryType>? categoryFaker = new Faker<CategoryType>()
                .RuleFor(x => x.Id, f => Guid.NewGuid())
                .RuleFor(x => x.Name, f => f.Company.CompanyName());



            var productFaker = new Faker<ProductType>()
                .RuleFor(x => x.Id, f => Guid.NewGuid())
                .RuleFor(x => x.Name, f => f.Commerce.ProductName())
                .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
                .RuleFor(x => x.Category, f => categoryFaker.Generate());

            return productFaker.Generate(10);
        }


        public List<ProductType> GetProducts()
        {
            return GetProductTypes();
        }

        public async Task<List<CategoryType>> GetCategories()
        {
            var categories = await categoryRepository.GetAll();
            return categories.Select(x => new CategoryType { Id = x.Id, Name = x.Name, Description = x.Description }).ToList();
        }

        public async Task<CategoryType?> GetCategory(Guid categoryId)
        {
            var category = await categoryRepository.GetById(categoryId);
            if(category == null)
                return null;
            return new CategoryType { Id = category.Id, Description = category.Description, Name = category.Name };
        }

        [GraphQLDeprecated("is deorecated")]
        public string Hello => "Hello OLMUG!";
    }
}
