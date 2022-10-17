namespace GraphQLDemo.API.Schema
{
    public class ProductType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryType Category { get; set; }
    }
}
