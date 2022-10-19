
using GraphQLDemo.API.DTO;
using GraphQLDemo.API.Services;
using HotChocolate.Subscriptions;

namespace GraphQLDemo.API.Schema
{
    public class Mutation
    {
        private List<CategoryType> _categories;
        private readonly CategoryRepository categoryRepository;

        public Mutation(CategoryRepository categoryRepository)
        {
            _categories = new List<CategoryType>();
            this.categoryRepository = categoryRepository;
        }

        public async Task<CategoryType> CreateCategory(string name, string description, [Service]ITopicEventSender topicEventSender)
        {
            var categoryDto = new CategoryDto()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description
            };
            categoryDto = await categoryRepository.Create(categoryDto);
            var category = new CategoryType
            {
                Id = categoryDto.Id,
                Description = categoryDto.Description,
                Name = categoryDto.Name
            };

            await topicEventSender.SendAsync(nameof(Subscription.CategoryCreated), category);

            return category;
        }

        public async Task<CategoryType> UpdateCategory(Guid id, string name, string description, [Service] ITopicEventSender topicEventSender)
        {
            var categoryDto = new CategoryDto()
            {
                Id = id,
                Name = name,
                Description = description
            };
            categoryDto = await categoryRepository.Update(categoryDto);

            var category = new CategoryType
            {
                Id = categoryDto.Id,
                Description = categoryDto.Description,
                Name = categoryDto.Name
            };

            var topicNane = $"{id}_{nameof(Subscription.CategoryUpdated)}";
            topicEventSender.SendAsync(topicNane, category);

            return category;
        }

        public bool DeleteCategory(Guid id, [Service] ITopicEventSender topicEventSender)
        {
            var result = _categories.RemoveAll(x => x.Id == id) >= 1;
            topicEventSender.SendAsync("Delete_Category", result);
            return result;
        }
    }
}
