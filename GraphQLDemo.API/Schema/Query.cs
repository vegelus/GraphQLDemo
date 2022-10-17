
using Bogus;

namespace GraphQLDemo.API.Schema
{
    public class Query
    {
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


        [GraphQLDeprecated("is deorecated")]
        public string Hello => "Hello OLMUG!";
    }
}
