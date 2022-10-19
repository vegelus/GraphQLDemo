using GraphQLDemo.API.Schema;
using GraphQLDemo.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>();
builder.Services.AddInMemorySubscriptions();
var connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddPooledDbContextFactory<DemoDbContext>(o => o.UseSqlite(connectionString).LogTo(Console.WriteLine));

builder.Services.AddScoped<CategoryRepository>();

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    var contextFactory = app.Services.GetRequiredService <IDbContextFactory<DemoDbContext>>();
    using( var dbContext = contextFactory.CreateDbContext())
    {
        dbContext.Database.Migrate();
    }
}

app.UseWebSockets();

app.MapGraphQL();

app.Run();
