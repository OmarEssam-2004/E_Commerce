using E_Commerce.Domain.Contracts;

namespace E_Commerce.API.Extenions
{
    public static class WebApplicationExtenions
    {
        public static async Task<WebApplication> SeedAndMigrationDataAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dataSeeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");
            var identitydataSeeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Identity");
            await dataSeeder.SeedDataAsync(); 
            await identitydataSeeder.SeedDataAsync(); 
            return app;
        }
    }
}
