using MCommerce.Catalog.API.DAL.Contracts;
using MCommerce.Catalog.API.Entities;
using MongoDB.Driver;

namespace MCommerce.Catalog.API.DAL
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }

        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("DBSettings").GetValue<string>("ConnectionString"));
            var database = client.GetDatabase(configuration.GetSection("DBSettings").GetValue<string>("DBName"));

            var test = configuration.GetSection("DBSettings").GetValue<string>("ConnectionString");

            Products = database.GetCollection<Product>(configuration.GetSection("DBSettings").GetValue<string>("CollectionName"));

            CatalogContextSeed.SeedData(Products);
        }        
    }
}
