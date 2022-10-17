using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace GraphQLDemo.API.Schema
{
    public class Subscription
    {
        [Subscribe]
        public CategoryType CategoryCreated([EventMessage] CategoryType category) => category;

        [Subscribe]
        [Topic("Delete_Category")]
        public bool CategoryDeleted([EventMessage] bool categoryDeleted) => categoryDeleted;

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<CategoryType>> CategoryUpdated(Guid categoryId, [Service] ITopicEventReceiver topicEventReceiver)
        {
            var topicNAme = $"{categoryId}_{nameof(CategoryUpdated)}";
            return topicEventReceiver.SubscribeAsync<string, CategoryType>(topicNAme);
        }
    }
}
