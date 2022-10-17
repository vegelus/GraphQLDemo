
using HotChocolate.Subscriptions;

namespace GraphQLDemo.API.Schema
{
    public class Mutation
    {
        private List<CategoryType> _categories;

        public Mutation()
        {
            _categories = new List<CategoryType>();
        }

        public async Task<CategoryType> CreateCategory(string name, string description, [Service]ITopicEventSender topicEventSender)
        {
            var category = new CategoryType()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description
            };
            _categories.Add(category);

            await topicEventSender.SendAsync(nameof(Subscription.CategoryCreated), category);

            return category;
        }

        public CategoryType UpdateCategory(Guid id, string name, string description, [Service] ITopicEventSender topicEventSender)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                throw new GraphQLException( new Error("Category not found", "CATEGORY_NOT_FOUND"));
            category.Name = name;
            category.Description = description;

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
