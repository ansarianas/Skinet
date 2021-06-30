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
                var basePath = "../Infrastructure/Data/SeedData";
                #region Seed data into productbrands table
                if (!context.ProductBrands.Any())
                {
                    var brands = ReadJsonAndMapToModel<ProductBrand>(basePath + "/brands.json");
                    await AddEntityToDb<ProductBrand>(context, brands);
                }
                #endregion

                #region Seed data into producttypes table
                if (!context.ProductTypes.Any())
                {
                    var types = ReadJsonAndMapToModel<ProductType>(basePath + "/types.json");
                    await AddEntityToDb<ProductType>(context, types);
                }
                #endregion

                #region Seed data into products table
                if (!context.Products.Any())
                {
                    var products = ReadJsonAndMapToModel<Product>(basePath + "/products.json");
                    await AddEntityToDb<Product>(context, products);
                }
                #endregion
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex, "An error occured while seeding data to tables");
            }
        }

        private static List<T> ReadJsonAndMapToModel<T>(string filePath) where T : class
        {
            var text = File.ReadAllText(filePath);
            var mappedJsonToModel = JsonSerializer.Deserialize<List<T>>(text);
            return mappedJsonToModel;
        }

        private static async Task AddEntityToDb<T>(StoreContext context, List<T> mappedJsonToModel) where T : BaseEntity
        {
            mappedJsonToModel.ForEach(x => context.Set<T>().Add(x));
            await context.SaveChangesAsync();
        }
    }
}