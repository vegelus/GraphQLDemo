
using GraphQLDemo.API.DTO;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.API.Services
{
    public class CategoryRepository
    {
        private readonly IDbContextFactory<DemoDbContext> _contextFactory;

        public CategoryRepository(IDbContextFactory<DemoDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<CategoryDto> Create(CategoryDto category)
        {
            using(var dbContext = _contextFactory.CreateDbContext())
            {
                dbContext.Categories.Add(category);
                await dbContext.SaveChangesAsync();
                return category;
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            using (var dbContext = _contextFactory.CreateDbContext())
            {
                return await dbContext.Categories.ToListAsync();
            }
        }

        public async Task<CategoryDto?> GetById(Guid categoryId)
        {
            using (var dbContext = _contextFactory.CreateDbContext())
            {
                return await dbContext.Categories.FindAsync(categoryId);
            }
        }

        public async Task<CategoryDto> Update(CategoryDto category)
        {
            using (var dbContext = _contextFactory.CreateDbContext())
            {
                dbContext.Categories.Update(category);
                await dbContext.SaveChangesAsync();
                return category;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            using (var dbContext = _contextFactory.CreateDbContext())
            {
                var category = new CategoryDto()
                {
                    Id = id
                };
                dbContext.Categories.Remove(category);
                return await dbContext.SaveChangesAsync() > 0;
            }

        }
    }
}
