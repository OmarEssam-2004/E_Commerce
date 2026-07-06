using E_Commerce.Domain.Common;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public class CatalogDataSeeder(StoreDbContext Context, ILogger<CatalogDataSeeder> Logger) : IDataSeeder
    {

        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var pendingMigrations = await Context.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    await Context.Database.MigrateAsync(ct);
                    Logger.LogInformation("Pending migrations applied successfully.");
                }

                var SeedrootPath = Path.Combine(AppContext.BaseDirectory, "DataSeeding");

                await SeedIfEmpty<ProductBrand, int>(SeedrootPath, "brands.json", ct);
                await SeedIfEmpty<ProductType, int>(SeedrootPath, "types.json", ct);
                await SeedIfEmpty<Product, int>(SeedrootPath, "products.json", ct);

                var count = await Context.SaveChangesAsync(ct);
                if (count > 0)
                    Logger.LogInformation($"{count} Rows Added");
                else
                    Logger.LogInformation($"Database Already Seeded or No New Data Added");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred during data seeding");
            }
        }

        private async Task SeedIfEmpty<TEntity, TKey>(string rootPath, string fileName, CancellationToken ct = default) where TEntity : BaseEntity<TKey>
        {
            if(Context.Set<TEntity>().Any())
            {
                var tableName = typeof(TEntity).Name;
                Logger.LogWarning($"Table {tableName} Already Has Data");
                return; 
            }

            var filePath = Path.Combine(rootPath, fileName);
            if (!File.Exists(filePath))
            {
                Logger.LogWarning($"File {fileName} Not Exists");
                return; 
            }

            using var filestream = File.OpenRead(filePath);
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            var data = await JsonSerializer.DeserializeAsync<List<TEntity>>(filestream, options, ct);
            if (data != null && data.Any())
            {
                await Context.Set<TEntity>().AddRangeAsync(data, ct);
            }







        }   

    }
}
