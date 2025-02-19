using Microsoft.EntityFrameworkCore;

using Application;
using Application.Interfaces;
using Application.ExternalServices;
using Infrastructure;
using Infrastructure.External;
using Infrastructure.Data;

namespace Api
{
    public static class Extensions
    {
        public static IServiceCollection InjectAppServices(this WebApplicationBuilder? builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            string? url = builder.Configuration["baseUrl"];
            string? apiKey = builder.Configuration["apiKey"];

            if (url == null)
                throw new ArgumentException(nameof(url));

            if (apiKey == null)
                throw new ArgumentException(nameof(apiKey));

            builder.Services.AddHttpClient<ICatsApiClient, CatsApiClient>(client =>
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", apiKey);
            });

            builder.Services.AddScoped<ICatServices, CatService>();
            builder.Services.AddScoped<ICatsRepository, CatsRepository>();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Register DbContext with DI
            builder.Services.AddDbContext<CatsDbContext>(options =>
                options.UseSqlServer(connectionString));
           
            return builder.Services;

        }
        public static async Task<IServiceScope> InitializePersistance(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CatsDbContext>();
            await dbContext.Database.MigrateAsync();

            return scope;
        }
    }
}