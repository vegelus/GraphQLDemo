using GraphQLDemo.API.DTO;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.API.Services
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {
        }

        public DbSet<ProductDto> Products { get; set; }
        public DbSet<CategoryDto> Categories { get; set; }
    }
}
