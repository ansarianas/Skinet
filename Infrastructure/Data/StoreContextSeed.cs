using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Core.Entities;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var basePath = "../Infrastructure/Data/SeedData/";
                #region Seed data into productbrands table
                if (!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText(basePath + "brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var brand in brands)
                    {
                        context.ProductBrands.Add(brand);
                    }

                    await context.SaveChangesAsync();
                }
                #endregion

                #region Seed data into producttypes table
                if (!context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText(basePath + "types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var type in types)
                    {
                        context.ProductTypes.Add(type);
                    }

                    await context.SaveChangesAsync();
                }
                #endregion

                #region Seed data into products table
                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText(basePath + "products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var product in products)
                    {
                        context.Products.Add(product);
                    }

                    await context.SaveChangesAsync();
                }
                #endregion
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex, "An error occured while seeding data to tables");
            }
        }
    }
}