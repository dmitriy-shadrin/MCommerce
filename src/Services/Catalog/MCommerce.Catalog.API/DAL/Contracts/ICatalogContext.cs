using MCommerce.Catalog.API.Entities;
using MongoDB.Driver;

namespace MCommerce.Catalog.API.DAL.Contracts
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
